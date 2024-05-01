using System;
using System.Collections.Generic;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam;

public class CloudSocialModule : Terraria.Social.Base.CloudSocialModule
{
	private const uint WRITE_CHUNK_SIZE = 1024u;
	private object ioLock = new object();
	private byte[] writeBuffer = new byte[1024];

	public override void Initialize()
	{
	}

	public override void Shutdown()
	{
	}

	public override IEnumerable<string> GetFiles()
	{
		lock (ioLock) {
			int fileCount = SteamRemoteStorage.GetFileCount();
			List<string> list = new List<string>(fileCount);
			for (int i = 0; i < fileCount; i++) {
				list.Add(SteamRemoteStorage.GetFileNameAndSize(i, out var _));
			}

			return list;
		}
	}

	public override bool Write(string path, byte[] data, int length)
	{
		lock (ioLock) {
			UGCFileWriteStreamHandle_t writeHandle = SteamRemoteStorage.FileWriteStreamOpen(path);
			for (uint num = 0u; num < length; num += 1024) {
				int num2 = (int)Math.Min(1024L, length - num);
				Array.Copy(data, num, writeBuffer, 0L, num2);
				SteamRemoteStorage.FileWriteStreamWriteChunk(writeHandle, writeBuffer, num2);
			}

			return SteamRemoteStorage.FileWriteStreamClose(writeHandle);
		}
	}

	public override int GetFileSize(string path)
	{
		lock (ioLock) {
			return SteamRemoteStorage.GetFileSize(path);
		}
	}

	public override void Read(string path, byte[] buffer, int size)
	{
		lock (ioLock) {
			SteamRemoteStorage.FileRead(path, buffer, size);
		}
	}

	public override bool HasFile(string path)
	{
		lock (ioLock) {
			return SteamRemoteStorage.FileExists(path);
		}
	}

	public override bool Delete(string path)
	{
		lock (ioLock) {
			return SteamRemoteStorage.FileDelete(path);
		}
	}

	public override bool Forget(string path)
	{
		lock (ioLock) {
			return SteamRemoteStorage.FileForget(path);
		}
	}
}
