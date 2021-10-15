using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary
{
	public class FoldersOperations
	{
		public delegate void OnException(Exception exception);
		/// <summary>
		/// Inform the caller a general exception was encountered
		/// </summary>
		public static event OnException OnExceptionEvent;

		public delegate void OnUnauthorizedAccessException(string message);
		/// <summary>
		/// Raised when attempting to access a folder the user does not have permissions too
		/// </summary>
		public static event OnUnauthorizedAccessException UnauthorizedAccessEvent;

		public delegate void OnTraverseFolder(string folderName);

		public static event OnTraverseFolder OnTraverseEvent;

		public delegate void OnTraverseExcludeFolder(string sender);
		/// <summary>
		/// Called each time a folder is being traversed
		/// </summary>
		public static event OnTraverseExcludeFolder OnTraverseIncludeFolderEvent;


        public delegate void ProcessFiles(string sender);

        public static event ProcessFiles ProcessFilesEvent;

        public static string[] FileExtensions { get; set; }


		public static bool Cancelled = false;

		/// <summary>
		/// Called with recursion until the condition has been satisfied, in this case iterating a folder structure.
		/// 
		/// If a <see cref="UnauthorizedAccessException"/> is thrown the exception propagated to any listeners using
		/// <see cref="UnauthorizedAccessEvent"/>
		///
		/// A <see cref="OperationCanceledException"/> is thrown if the operation is cancelled by a cancel request.
		/// 
		/// </summary>
		/// <param name="directoryInfo"></param>
		/// <param name="fileExtensions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public static async Task RecursiveFolders(DirectoryInfo directoryInfo, string[] fileExtensions, CancellationToken cancellationToken)
		{

			if (!directoryInfo.Exists)
			{
				OnTraverseEvent?.Invoke("Nothing to process");

				return;
			}

            FileExtensions = fileExtensions;

			if (fileExtensions.Any(directoryInfo.FullName.Contains))
			{
				await Task.Delay(1);
				OnTraverseEvent?.Invoke(directoryInfo.FullName);
			}
			else
			{
				OnTraverseIncludeFolderEvent?.Invoke(directoryInfo.FullName);
			}

			DirectoryInfo folder = null;

			try
			{
				await Task.Run(async () =>
				{
					foreach (DirectoryInfo dir in directoryInfo.EnumerateDirectories())
					{

						folder = dir;

						if (
                            (folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || 
                            (folder.Attributes & FileAttributes.System) == FileAttributes.System || 
                            (folder.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint) {
							
                            OnTraverseIncludeFolderEvent?.Invoke($"* {folder.FullName}");

							continue;

						}

						if (!Cancelled)
						{

							await Task.Delay(1, cancellationToken);

							// recurse 
							await RecursiveFolders(folder, fileExtensions, cancellationToken);

						}
						else
						{
							return;
						}

						if (cancellationToken.IsCancellationRequested)
						{
							cancellationToken.ThrowIfCancellationRequested();
						}

					}
				}, cancellationToken);

			}
			catch (Exception ex)
			{
				if (ex is OperationCanceledException)
				{
					Cancelled = true;
				}
				else if (ex is UnauthorizedAccessException)
				{
					UnauthorizedAccessEvent?.Invoke($"Access denied '{ex.Message}'");
				}
				else
				{
					OnExceptionEvent?.Invoke(ex);
				}
			}
		}

		public static void RecursiveFolders(string path, int indentLevel)
		{

			try
			{

				if ((File.GetAttributes(path) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
				{

					foreach (string folder in Directory.GetDirectories(path))
					{
						Debug.WriteLine($"{new string(' ', indentLevel)}{Path.GetFileName(folder)}");
						RecursiveFolders(folder, indentLevel + 2);
					}

				}

			}
			catch (UnauthorizedAccessException unauthorized)
			{
				Debug.WriteLine($"{unauthorized.Message}");
			}
		}
		/// <summary>
		/// Iterate folder, get files by extension
		/// </summary>
		/// <param name="targetDirectory">existing folder</param>
        public static void ProcessDirectory(string targetDirectory)
        {
            var dirInfo = new DirectoryInfo(targetDirectory).GetFilesByExtensions(FileExtensions);
            if (dirInfo.Any())
            {
                foreach (var info in dirInfo)
                {
                    ProcessFile(info.Name);
                }
            }


            // Recurse into subdirectories of this directory.
            string[] subDirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subDirectory in subDirectoryEntries)
            {
                ProcessDirectory(subDirectory);
            }
        }

		/// <summary>
		/// Alert listeners of file found using <see cref="ProcessDirectory"/>
		/// </summary>
		/// <param name="path"></param>
		public static void ProcessFile(string path)
        {
            ProcessFilesEvent?.Invoke($"\t{Path.GetFileName(path)}");
		}

	}
}

