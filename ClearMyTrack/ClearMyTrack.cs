using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ClearMyTracks
{
    /*
     * To clear IE temporary Internet files – ClearMyTracksByProcess 8
     * To clear IE browsing cookies – ClearMyTracksByProcess 2
     * To clear IE browsing history – ClearMyTracksByProcess 193 (ALSO deletes add on history)
     * To clear IE form data- ClearMyTracksByProcess 16
     * To clear IE remembered passwords for filling web login forms-ClearMyTracksByProcess 32
     * To clear or delete all IE browsing history – all of above!- ClearMyTracksByProcess 255
     * To clear IE Tracking- ClearMyTracksByProcess 2048
     * Preserve Favourites use 8192
     * To clear IE Downloaded Files- ClearMyTracksByProcess 16384 
     * http://www.howtogeek.com/howto/windows/clear-ie7-browsing-history-from-the-command-line/
     */

    public enum ClearFlags
    {
        DeleteCookies = 2,
        DeleteHistoryFiles = 8,
        DeleteFormData = 16,
        DeletePasswords = 32,
        DeleteHistory = 193,
        DeleteALLHistory = 255,
        DeleteTrackingInfo = 2048,
        PreserveFavourites = 8192,
        DeleteDownloadHistory = 16384,
        DeleteEverything = 22783
    };

    public static void IEClearHistory(bool PreserveFavs, bool TempFiles, bool Cookies, bool History, bool form, bool passwords, bool downloads)
    {
        uint mask = 0;

        if (PreserveFavs)
            mask |= (uint)ClearFlags.PreserveFavourites;

        if (TempFiles)
            mask |= (uint)ClearFlags.DeleteHistoryFiles;

        if (Cookies)
            mask |= (uint)ClearFlags.DeleteCookies;

        if (History)
            mask |= (uint)ClearFlags.DeleteHistory;

        if (form)
            mask |= (uint)ClearFlags.DeleteFormData;

        if (passwords)
            mask |= (uint)ClearFlags.DeletePasswords;

        if (downloads)
            mask |= (uint)ClearFlags.DeleteDownloadHistory;

        if (mask != 0)
            System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess " + mask.ToString());
    }
}
