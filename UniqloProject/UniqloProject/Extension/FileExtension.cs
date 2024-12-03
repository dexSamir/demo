﻿using System;
namespace UniqloProject.Extension
{
	public static class FileExtension
	{
		public static bool IsValidType(this IFormFile file, string type)
			=> file.ContentType.StartsWith(type);
		public static bool IsValidSize(this IFormFile file, int kb)
			=> file.Length <= kb * 1024;

		public static async Task<string> UploadAsync(this IFormFile file, params string[] paths)
		{
			string path = Path.Combine(paths);
			if (!Path.Exists(path))
				Directory.CreateDirectory(path);

			string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
			using (Stream sr = File.Create(Path.Combine(path, fileName)))
				await file.CopyToAsync(sr);
			return fileName; 
		}
	}
}

