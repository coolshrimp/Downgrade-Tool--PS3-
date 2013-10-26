Imports System.IO
Public Class Downloader
    Inherits System.Windows.Forms.Form
    Public FileToDownload As String
    Public SaveLocation As String
    'DECLARE THIS WITHEVENTS SO WE GET EVENTS ABOUT DOWNLOAD PROGRESS
    Private WithEvents Downloader As WebFileDownloader

    'SUB MAIN WHERE WE ENABLE VISUAL STYLES, AND RUN MAIN FORM
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New Downloader)
        Application.Exit()
    End Sub

    'CLOSE PROGRAM
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    'START DOWNLOAD
    Private Sub StartDownload()

        'VERIFY A DIRECTORY WAS PICKED AND THAT IT EXISTS
        If Not IO.Directory.Exists(SaveLocation) Then
            MessageBox.Show("Not a valid directory to download to", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DownloadingLBL.Text = "Download Failed"
            Return
        End If

        'DO THE DOWNLOAD
        Try
            cmdClose.Enabled = False
            Downloader = New WebFileDownloader
            Downloader.DownloadFileWithProgress(FileToDownload, SaveLocation.TrimEnd("\"c) & GetFileNameFromURL(FileToDownload))
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    'returns all text from last "/" in URL, but puts a "\" in front of the file name..
    Private Function GetFileNameFromURL(ByVal URL As String) As String
        If URL.IndexOf("/"c) = -1 Then Return String.Empty

        Return "\" & URL.Substring(URL.LastIndexOf("/"c) + 1)
    End Function

    'FIRES WHEN WE HAVE GOTTEN THE DOWNLOAD SIZE, SO WE KNOW WHAT BOUNDS TO GIVE THE PROGRESS BAR
    Private Sub Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles Downloader.FileDownloadSizeObtained
        ProgBar.Value = 0
        ProgBar.Maximum = Convert.ToInt32(iFileSize)
    End Sub

    'FIRES WHEN DOWNLOAD IS COMPLETE
    Private Sub Downloader_FileDownloadComplete() Handles Downloader.FileDownloadComplete
        ProgBar.Value = ProgBar.Maximum
        DownloadingLBL.Text = "File Download Complete"
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
        cmdClose.Enabled = True
        If FileToDownload = "Http://coolshrimpmodz.com/DowngradeTool/DowngradeToolSetup.exe" Then
            If File.Exists(SaveLocation & "\DowngradeToolSetup.exe") = True Then
                Process.Start(SaveLocation & "\DowngradeToolSetup.exe")
                Downgrade_Tool.Main.Close()
            End If
        End If
    End Sub

    'FIRES WHEN DOWNLOAD FAILES. PASSES IN EXCEPTION INFO
    Private Sub Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles Downloader.FileDownloadFailed
        DownloadingLBL.Text = "Download Failed"
        If File.Exists(SaveLocation.TrimEnd("\"c) & GetFileNameFromURL(FileToDownload)) Then
            File.Delete(SaveLocation.TrimEnd("\"c) & GetFileNameFromURL(FileToDownload))
        End If
        MessageBox.Show("An error has occured during download: " & ex.Message)
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
        cmdClose.Enabled = True
    End Sub

    'FIRES WHEN MORE OF THE FILE HAS BEEN DOWNLOADED
    Private Sub Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles Downloader.AmountDownloadedChanged
        ProgBar.Value = Convert.ToInt32(iNewProgress)
        lblProgress.Text = WebFileDownloader.FormatFileSize(iNewProgress) & " of " & WebFileDownloader.FormatFileSize(ProgBar.Maximum) & " downloaded"
        Application.DoEvents()
    End Sub

    Private Sub Downloader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblProgress.Text = String.Empty
    End Sub

    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        Call StartDownload()
    End Sub
End Class
