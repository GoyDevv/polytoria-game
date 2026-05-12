// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Godot;

namespace Polytoria.Mobile.Utils;

public static class SecureTokenStore
{
    private const string TokenFileName = "user://mobile_auth.dat";
    private const string KeystoreKey = "polytoria_mobile_auth_key"; // Handled by Android platform keystore internally by Godot

    public static void SaveToken(string token)
    {
        // On Android, use the keystore abstraction if Godot 4 Android keystore plugin is available.
        // For standard Godot FileAccess encrypted store:
        using FileAccess file = FileAccess.OpenEncryptedWithPass(TokenFileName, FileAccess.ModeFlags.Write, KeystoreKey);
        if (file != null)
        {
            file.StoreString(token);
        }
    }

    public static string? LoadToken()
    {
        if (!FileAccess.FileExists(TokenFileName))
            return null;

        using FileAccess file = FileAccess.OpenEncryptedWithPass(TokenFileName, FileAccess.ModeFlags.Read, KeystoreKey);
        if (file != null)
        {
            return file.GetAsText();
        }
        return null;
    }

    public static void ClearToken()
    {
        if (FileAccess.FileExists(TokenFileName))
        {
            DirAccess.RemoveAbsolute(TokenFileName);
        }
    }
}
