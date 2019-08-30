using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using WildcardMatch;

namespace Unimpressive.Core
{
    public static class FileSystem
    {
        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;

        /// <summary>
        /// Returns whether the path is absolute.
        /// </summary>
        /// <param name="path">The path to check.</param>
        public static bool IsAbsolutePath([NotNull] string path)
        {
            return new Uri(path, UriKind.RelativeOrAbsolute).IsAbsoluteUri;
        }

        /// <summary>
        /// Returns whether the path is relative.
        /// </summary>
        /// <param name="path">The path to check.</param>
        public static bool IsRelativePath([NotNull] string path)
        {
            return !IsAbsolutePath(path);
        }

        /// <summary>
        /// Converts a URL relative to the <paramref name="basePath" /> to absolute URL.
        /// </summary>
        /// <param name="basePath">The absolute base path.</param>
        /// <param name="relativePath">The relative path to convert to absolute.</param>
        public static string MakeAbsolute([NotNull] string basePath, [NotNull] string relativePath)
        {
            if (IsAbsolutePath(relativePath))
            {
                return relativePath;
            }

            basePath = Path.GetDirectoryName(basePath);

            if (!IsAbsolutePath(basePath))
            {
                throw new InvalidOperationException("The base file path is not absolute.");
            }

            relativePath = RemoveLeadingSlash(relativePath);

            var splittedPath = relativePath.Split('?');
            relativePath = splittedPath[0];

            var absolutePath = Path.Combine(basePath, relativePath);
            absolutePath = new Uri(absolutePath).LocalPath;

            if (splittedPath.Length > 1)
            {
                absolutePath += "?" + splittedPath[1];
            }

            return Normalize(absolutePath);
        }

        /// <summary>
        /// Converts the absolute file path to server-relative file path.
        /// </summary>
        /// <param name="rootPath">The absolute root path.</param>
        /// <param name="filePath">The absolute file path.</param>
        public static string MakeRelativeToRoot([NotNull] string rootPath, [NotNull] string filePath)
        {
            var splittedPath = filePath.Split('?');
            filePath = splittedPath[0];

            var rootUri = new Uri(rootPath);
            var fileUri = new Uri(filePath);

            if (!rootUri.IsAbsoluteUri)
            {
                throw new Exception("The root path is not absolute.");
            }
            if (!fileUri.IsAbsoluteUri)
            {
                throw new Exception("The file path is not absolute.");
            }

            if (!rootUri.IsBaseOf(fileUri))
            {
                throw new Exception("The root path is not base of the file path.");
            }

            var relativePath = "/" + rootUri.MakeRelativeUri(fileUri);

            if (splittedPath.Length > 1)
            {
                relativePath += "?" + splittedPath[1];
            }

            return Normalize(relativePath);
        }

        /// <summary>
        /// Normalizes the specified file path.
        /// </summary>
        /// <param name="path">The path to normalize.</param>
        public static string Normalize(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (path.StartsWith(@"\??\", StringComparison.Ordinal))
            {
                path = path.Substring(4);
            }

            path = ReplaceBackSlashes(path);
            path = RemoveRedundantSlashes(path);
            return path;
        }

        /// <summary>
        /// Prepends leading slash.
        /// </summary>
        /// <param name="path">The file / folder path.</param>
        public static string PrependLeadingSlash([NotNull] string path)
        {
            path = Normalize(path);

            if ((path.Length == 0) || (path[0] != '/'))
            {
                path = "/" + path;
            }

            return path;
        }

        /// <summary>
        /// Appends trailing slash to the given path.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        public static string AppendTrailingSlash([NotNull] string folderPath)
        {
            folderPath = Normalize(folderPath);
            var lastIndex = folderPath.Length - 1;

            if ((folderPath.Length == 0) || (folderPath[lastIndex] != '/'))
            {
                folderPath += "/";
            }

            return folderPath;
        }

        /// <summary>
        /// Removes leading slash.
        /// </summary>
        /// <param name="path">The file / folder path.</param>
        public static string RemoveLeadingSlash([NotNull] string path)
        {
            path = Normalize(path);

            if (path.StartsWith("/", StringComparison.Ordinal))
            {
                path = path.Substring(1);
            }
            return path;
        }

        /// <summary>
        /// Removes trailing slash.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        public static string RemoveTrailingSlash([NotNull] string folderPath)
        {
            folderPath = Normalize(folderPath);

            if (string.IsNullOrEmpty(folderPath))
            {
                return folderPath;
            }

            var lastIndex = folderPath.Length - 1;

            if (folderPath[lastIndex] == '/')
            {
                folderPath = folderPath.Substring(0, lastIndex);
            }

            return folderPath;
        }

        private static string RemoveRedundantSlashes([NotNull] string path)
        {
            const string schemeSep = "://";

            var schemeSepPosition = path.IndexOf(schemeSep, StringComparison.Ordinal);
            var scheme = string.Empty;

            if (schemeSepPosition >= 0)
            {
                scheme = path.Substring(0, schemeSepPosition);
                path = path.Substring(schemeSepPosition + schemeSep.Length);
            }

            while (path.Contains("//"))
            {
                path = path.Replace("//", "/");
            }

            if (schemeSepPosition >= 0)
            {
                path = scheme + schemeSep + path;
            }

            return path;
        }

        private static string ReplaceBackSlashes([NotNull] string path)
        {
            return path.Replace("\\", "/");
        }

        private static int GetPathAttribute(string path, int defaultAttr)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                return FILE_ATTRIBUTE_DIRECTORY;
            }

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                return FILE_ATTRIBUTE_NORMAL;
            }

            return defaultAttr;
        }

        /// <summary>
        /// Intuitively recursively copy a directory with filters. This is a blocking function
        /// </summary>
        /// <param name="src">Source folder</param>
        /// <param name="dst">Destination folder</param>
        /// <param name="ignore">blacklist files or patterns</param>
        /// <param name="match">whitelist files or patterns</param>
        /// <param name="progress">an optional callback function on progress change</param>
        /// <param name="error">an optional callback function on error</param>
        public static void CopyDirectory(
            string src,
            string dst,
            string[] ignore = null,
            string[] match = null,
            Action<FileSystemInfo> progress = null,
            Action<Exception> error = null)
        {


            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(src);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + src);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (match != null)
            {
                dirs = dirs.Where(info =>
                {
                    return match.Any(pattern => pattern.WildcardMatch(info.Name));
                }).ToArray();
            }
            if (ignore != null)
            {
                dirs = dirs.Where(info =>
                {
                    return !ignore.Any(pattern => pattern.WildcardMatch(info.Name));
                }).ToArray();
            }
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();

            if (match != null)
            {
                files = files.Where(info =>
                {
                    return match.Any(pattern => pattern.WildcardMatch(info.Name));
                }).ToArray();
            }
            if (ignore != null)
            {
                files = files.Where(info =>
                {
                    return !ignore.Any(pattern => pattern.WildcardMatch(info.Name));
                }).ToArray();
            }

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(dst, file.Name);
                progress?.Invoke(file);
                try
                {
                    file.CopyTo(temppath, true);
                }
                catch
                {
                    try
                    {
                        if (File.Exists(temppath))
                        {
                            var attrs = File.GetAttributes(temppath);
                            if ((attrs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            {
                                attrs = attrs & ~FileAttributes.ReadOnly;
                                File.SetAttributes(temppath, attrs);
                            }
                            if ((attrs & FileAttributes.Hidden) == FileAttributes.Hidden)
                            {
                                attrs = attrs & ~FileAttributes.Hidden;
                                File.SetAttributes(temppath, attrs);
                            }
                            file.CopyTo(temppath, true);
                        }
                    }
                    catch (Exception e)
                    {
                        error?.Invoke(e);
                    }
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(dst, subdir.Name);
                progress?.Invoke(subdir);
                try
                {
                    CopyDirectory(subdir.FullName, temppath, ignore, match, progress);
                }
                catch (Exception e)
                {
                    error?.Invoke(e);
                }
            }
        }

        /// <summary>
        /// Intuitively delete a directory and its contents
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        public static void DeleteDirectory(
            string path,
            bool recursive,
            string[] ignore = null,
            string[] match = null,
            Action<string> progress = null,
            Action<Exception> error = null)
        {
            if (recursive)
            {

                var dirs = Directory.GetDirectories(path);

                if (match != null)
                {
                    dirs = dirs.Where(dir =>
                    {
                        return match.Any(pattern => pattern.WildcardMatch(dir));
                    }).ToArray();
                }
                if (ignore != null)
                {
                    dirs = dirs.Where(dir =>
                    {
                        return !ignore.Any(pattern => pattern.WildcardMatch(dir));
                    }).ToArray();
                }

                foreach (var s in dirs)
                {
                    progress?.Invoke(s);
                    DeleteDirectory(s, true);
                }
            }

            var files = Directory.GetFiles(path);

            if (match != null)
            {
                files = files.Where(file =>
                {
                    return match.Any(pattern => pattern.WildcardMatch(file));
                }).ToArray();
            }
            if (ignore != null)
            {
                files = files.Where(file =>
                {
                    return !ignore.Any(pattern => pattern.WildcardMatch(file));
                }).ToArray();
            }

            foreach (var f in files)
            {
                progress?.Invoke(f);
                try
                {
                    var attr = File.GetAttributes(f);
                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                    }
                    File.Delete(f);
                }
                catch (Exception e)
                {
                    error?.Invoke(e);
                }
            }
            try
            {
                if(Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
                    Directory.Delete(path);
            }
            catch (Exception e)
            {
                error?.Invoke(e);
            }
        }
    }
}
