#Region "Imports"
Imports System
Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Security
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports AutoUpdaterDotNET
Imports System.IO.Ports
Imports System.Reflection

#End Region

Public Class Main
    Public MakeCMD As StreamWriter
    Public ReadTimes, PicHight, PicWidth As Integer
    Public CMD, InstLoc, NandCommand, NandPatchedFileName, NandNumber, NandReadOffset, NandWriteOffset, WriteMode, DiffWriteFile As String
    Public System32Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.System)
    Public Desktop As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    Public ComPort As String = My.Settings.ComPortDD
    Public NandVerify As String = " "

    Private Shared Function CurrentDomainAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        If String.IsNullOrEmpty(args.Name) Then
            Throw New Exception("DLL Read Failure (Nothing to load!)")
        End If
        Dim name As String = String.Format("{0}.dll", args.Name.Split(","c)(0))
        Using stream = Assembly.GetAssembly(GetType(Main)).GetManifestResourceStream(String.Format("{0}.{1}", GetType(Main).Namespace, name))
            If stream IsNot Nothing Then
                Dim data = New Byte(stream.Length - 1) {}
                stream.Read(data, 0, data.Length)
                Return Assembly.Load(data)
            End If
            Throw New Exception(String.Format("Can't find external nor internal {0}!", name))
        End Using
    End Function

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomainAssemblyResolve
        ComPortDD.DataSource = SerialPort.GetPortNames()
        Me.Text = Me.Text + " v" + My.Application.Info.Version.ToString
        InstLoc = Directory.GetCurrentDirectory()
        ReadTimes = 0
        ImageSelection.SelectedIndex = 0
        ComPort = My.Settings.ComPortDD
        AutoUpdater.Start("Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/DowngradeToolVersion.xml")
    End Sub

#Region "Nand Tab"
    Private Sub NandTypeDD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles NandTypeDD.SelectedIndexChanged
        If NandTypeDD.Text = "NOR" Then
            NandCommand = "NORway.py "
            NandNumber = " "
            NandPatchedFileName = "NOR_patched.bin"
        ElseIf NandTypeDD.Text = "NAND" Then
            NandCommand = "NANDway.py "
            NandPatchedFileName = "NAND_patched.bin"
            NandNumber = " 0 "
        End If
    End Sub

    Private Sub NandChipDD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WriteModeDD.SelectedIndexChanged
        If WriteModeDD.SelectedIndex = 0 Then
            WriteMode = "write "
            DiffWriteFile = ""
        ElseIf WriteModeDD.SelectedIndex = 1 Then
            WriteMode = "writeword "
            DiffWriteFile = ""
        ElseIf WriteModeDD.SelectedIndex = 2 Then
            WriteMode = "diffwrite "
            DiffWriteFile = " NandDiff.txt"
        End If

    End Sub

    Private Sub ReadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadButton.Click
        If NandCommand = "" Or ComPort = "" Or WriteMode = "" Then
            MsgBox("Make sure you have selected ComPort, Nand Type and Nand Chip")
        Else
            MakeCMD = New StreamWriter("CMD.bat", False)
            MakeCMD.WriteLine("@echo off")
            MakeCMD.WriteLine(NandCommand & ComPort & NandNumber & "dump OFW.bin" & NandReadOffset) 'Do Read Command in CMD Prompt
            MakeCMD.WriteLine(NandCommand & ComPort & NandNumber & "release") 'Do Read Command in CMD Prompt
            MakeCMD.WriteLine("pause")
            MakeCMD.Close()
            CMD = "CMD.bat"
            Shell(CMD, vbNormalFocus, True)
            If File.Exists("OFW.bin") = True Then
                'Save  File Box
                SaveFile.Title = "Select Save Location"
                SaveFile.FileName = "Nand"
                SaveFile.ShowDialog()
                My.Computer.FileSystem.CurrentDirectory = InstLoc
                File.Copy("OFW.bin", SaveFile.FileName, True)
                MsgBox("Reading Done Saved To:" & vbCrLf & SaveFile.FileName) 'Notify Finished!!
                ReadTimes += 1
                If ReadTimes = 1 Then
                    Nand1TextBox.Text = SaveFile.FileName
                ElseIf ReadTimes = 2 Then
                    Nand2TextBox.Text = SaveFile.FileName
                    CompareNandButton.Enabled = True
                    CompareNandButton.PerformClick()
                ElseIf ReadTimes > 2 Then
                    ReadTimes = 1
                    Nand1TextBox.Text = SaveFile.FileName
                End If
                If File.Exists("OFW.Bin") = True Then
                    File.Delete("OFW.Bin")
                End If
            Else
                MsgBox("Reading Error Try Again") 'Notify Error!!
            End If
            If File.Exists("CMD.bat") = True Then
                File.Delete("CMD.bat")
            End If
        End If
        SaveFile.FileName = ""
    End Sub

#Region "Compare"
    Private Sub Nand1Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nand1Button.Click
        Dim open As New OpenFileDialog()
        open.Filter = "Bin files (*.bin)|*.bin|All Files (*.*)|*.*"
        open.FilterIndex = 1
        open.CheckFileExists = True
        open.RestoreDirectory = True
        If open.ShowDialog() = DialogResult.OK Then
            Nand1TextBox.Text = open.FileName
        End If
        If Nand1TextBox.Text.Length > 0 AndAlso Nand2TextBox.Text.Length > 0 Then
            CompareNandButton.Enabled = True
        End If
    End Sub
    Private Sub Nand1TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nand1TextBox.TextChanged
        CompareNandProgress.Value = 0
    End Sub

    Private Sub Nand2Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nand2Button.Click
        Dim open As New OpenFileDialog()
        open.Filter = "Bin files (*.bin)|*.bin|All Files (*.*)|*.*"
        open.FilterIndex = 1
        open.CheckFileExists = True
        open.RestoreDirectory = True
        If open.ShowDialog() = DialogResult.OK Then
            Nand2TextBox.Text = open.FileName
        End If

        If Nand1TextBox.Text.Length > 0 AndAlso Nand2TextBox.Text.Length > 0 Then
            CompareNandButton.Enabled = True
        End If
    End Sub
    Private Sub Nand2TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nand2TextBox.TextChanged
        CompareNandProgress.Value = 0
    End Sub

    Private Sub CompareNandButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompareNandButton.Click
        CompareResultTextBox.Text = ""
        CompareNandButton.Enabled = False
        Application.DoEvents()
        Cursor = Cursors.WaitCursor

        Dim inStream1 As Stream = Nothing
        Dim inStream2 As Stream = Nothing

        Try
            inStream1 = File.Open(Nand1TextBox.Text, FileMode.Open, FileAccess.Read)
            inStream2 = File.Open(Nand2TextBox.Text, FileMode.Open, FileAccess.Read)

            If inStream1.Length <> inStream2.Length Then
                MessageBox.Show("File lengths do not match!")
                Return
            End If

            Dim blockSize As Integer = 16384
            If Not ValidFileSize(inStream1.Length, blockSize) Then
                MessageBox.Show("Wrong file lengths!" & blockSize)
                Return
            End If

            Dim nBlocks As Integer = CInt(inStream1.Length \ blockSize)
            'CompareResultTextBox.Text = nBlocks & " block(s) in nand image" & vbCr & vbLf
            CompareNandProgress.Value = 0
            CompareNandProgress.Maximum = nBlocks
            Dim blocks As ArrayList = Compare(inStream1, inStream2, nBlocks, blockSize)

            CompareResultTextBox.Text = blocks.Count & " non-matching block(s) found" & vbCr & vbLf
            Dim bad As Integer = 0
            For Each i As Integer In blocks
                If bad = 100 Then
                    CompareResultTextBox.Text += vbCr & vbLf & "List truncated - too many!" & vbCr & vbLf
                ElseIf bad < 100 Then
                    CompareResultTextBox.Text += i.ToString("X") & vbCr & vbLf
                End If
                bad += 1

            Next
        Catch ex As Exception
            Cursor = Cursors.[Default]
            'MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "Nand Compare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(ex.Message, "Nand Compare", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If inStream1 IsNot Nothing Then
                inStream1.Close()
            End If
            If inStream2 IsNot Nothing Then
                inStream2.Close()
            End If
            CompareNandButton.Enabled = True
            Cursor = Cursors.[Default]
        End Try
    End Sub
    Private Function Compare(ByVal stream1 As Stream, ByVal stream2 As Stream, ByVal nBlocks As Integer, ByVal blockSize As Integer) As ArrayList

        Dim blocks As New ArrayList()

        Dim data1 As Byte() = New Byte(blockSize - 1) {}
        Dim data2 As Byte() = New Byte(blockSize - 1) {}

        stream1.Position = 0
        stream2.Position = 0

        For i As Integer = 0 To nBlocks - 1
            stream1.Read(data1, 0, blockSize)
            stream2.Read(data2, 0, blockSize)
            For j As Integer = 0 To blockSize - 1
                If data1(j) <> data2(j) Then
                    blocks.Add(i)
                    Exit For
                End If
            Next
            CompareNandProgress.PerformStep()
            Application.DoEvents()
        Next

        Return blocks

    End Function
    Private Function ValidFileSize(ByVal size As Long, ByVal blockSize As Integer) As Boolean
        If (size Mod blockSize) = 0 Then
            Return True
        Else
            Return False
        End If
        'if (size == 17301504) {
        '                // 16mb
        '                return true;
        '            } else if (size == 276824064 || size == 553648128) {
        '                // 256mb & 512mb
        '                return true;
        '            } else if (size == 69206016 || size == 75694080) {
        '                // 64mb & 70mb (truncated large nands)
        '                return true;
        '            } else {
        '                return false;
        '            }

    End Function
#End Region

    Private Sub CheckPatchNand_Click(sender As Object, e As EventArgs) Handles CheckPatchNand.Click
        'Select  File Box
        SelectFile.Title = "Select Image"
        SelectFile.FileName = ""
        SelectFile.ShowDialog()
        My.Computer.FileSystem.CurrentDirectory = InstLoc
        If SelectFile.FileName = "" Then ' If No File Was Selected
            MsgBox("No File Was Selected")
        Else
            If File.Exists(SelectFile.FileName) = False Then
                MsgBox("Cannot Find Selected File")
            Else
                File.Copy(SelectFile.FileName, "OFW.bin", True)
                If File.Exists("OFW.bin") = True Then
                    Diagnostics.Process.Start(InstLoc & "\checktool.exe", "OFW.bin").WaitForExit()
                    If File.Exists(NandPatchedFileName) = True Then
                        'Save  File Box
                        SaveFile.Title = "Select Save Location"
                        SaveFile.FileName = "CFW"
                        SaveFile.ShowDialog()
                        My.Computer.FileSystem.CurrentDirectory = InstLoc
                        File.Copy(NandPatchedFileName, SaveFile.FileName, True)
                        MsgBox("File Patched Saved To:" & vbCrLf & SaveFile.FileName) 'Notify Finished!!
                    Else
                        MsgBox("Error Patching Try Again") 'Notify Error!!
                    End If
                    If File.Exists("CMD.bat") = True Then
                        File.Delete("CMD.bat")
                    End If
                    If File.Exists("OFW.Bin") = True Then
                        File.Delete("OFW.Bin")
                    End If
                    If File.Exists(NandPatchedFileName) = True Then
                        File.Delete(NandPatchedFileName)
                    End If
                    SaveFile.FileName = ""
                Else
                    MsgBox("Cannot Find Selected File")
                End If
            End If
        End If
    End Sub
    Private Sub CheckPatchNand_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles CheckPatchNand.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim message = MessageBox.Show("Are you sure you want to patch nand without checking?", "Patch?", MessageBoxButtons.YesNo)
            If message = DialogResult.Yes Then
                'Select  File Box
                SelectFile.Title = "Select Image"
                SelectFile.FileName = ""
                SelectFile.ShowDialog()
                My.Computer.FileSystem.CurrentDirectory = InstLoc
                If SelectFile.FileName = "" Then ' If No File Was Selected
                    MsgBox("No File Was Selected")
                Else
                    If File.Exists(SelectFile.FileName) = False Then
                        MsgBox("Cannot Find Selected File")
                    Else
                        File.Copy(SelectFile.FileName, "OFW.bin", True)
                        If File.Exists("OFW.bin") = True Then
                            Diagnostics.Process.Start(InstLoc & "\patcher.exe", "OFW.bin").WaitForExit()
                            If File.Exists(NandPatchedFileName) = True Then
                                'Save  File Box
                                SaveFile.Title = "Select Save Location"
                                SaveFile.FileName = "CFW"
                                SaveFile.ShowDialog()
                                My.Computer.FileSystem.CurrentDirectory = InstLoc
                                File.Copy(NandPatchedFileName, SaveFile.FileName, True)
                                MsgBox("File Patched Saved To:" & vbCrLf & SaveFile.FileName) 'Notify Finished!!
                            Else
                                MsgBox("Error Patching Try Again") 'Notify Error!!
                            End If
                            If File.Exists("CMD.bat") = True Then
                                File.Delete("CMD.bat")
                            End If
                            If File.Exists("OFW.Bin") = True Then
                                File.Delete("OFW.Bin")
                            End If
                            If File.Exists(NandPatchedFileName) = True Then
                                File.Delete(NandPatchedFileName)
                            End If
                            SaveFile.FileName = ""
                        Else
                            MsgBox("Cannot Find Selected File")
                        End If
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub CheckCon_Click(sender As Object, e As EventArgs) Handles CheckCon.Click
        If NandCommand = "" Or ComPort = "" Or WriteMode = "" Then
            MsgBox("Make sure you have selected ComPort, Nand Type and Nand Chip")
        Else
            MakeCMD = New StreamWriter("CMD.bat", False)
            MakeCMD.WriteLine("@echo off")
            MakeCMD.WriteLine(NandCommand & ComPort) 'Do Read Command in CMD Prompt
            MakeCMD.WriteLine(NandCommand & ComPort & NandNumber & "release") 'Do Read Command in CMD Prompt
            MakeCMD.WriteLine("pause")
            MakeCMD.Close()
            CMD = "CMD.bat"
            Shell(CMD, vbNormalFocus, True)
            If File.Exists("CMD.bat") = True Then
                File.Delete("CMD.bat")
            End If
        End If
    End Sub

    Private Sub InstallCFWButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallCFWButton.Click
        If NandCommand = "" Or ComPort = "" Or WriteMode = "" Then
            MsgBox("Make sure you have selected ComPort, Nand Type and Nand Chip")
        Else
            'Select  File Box
            SelectFile.Title = "Select Image"
            SelectFile.FileName = ""
            SelectFile.ShowDialog()
            My.Computer.FileSystem.CurrentDirectory = InstLoc
            If SelectFile.FileName = "" Then ' If No File Was Selected
                MsgBox("No File Was Selected")
            Else
                If File.Exists(SelectFile.FileName) = False Then
                    MsgBox("Cannot Open Selected File")
                Else
                    File.Copy(SelectFile.FileName, "CFW.bin", True)
                    If DiffWriteFile <> "" Then
                        SelectDiff.Title = "Select Diff File"
                        SelectDiff.FileName = ""
                        SelectDiff.ShowDialog()
                        My.Computer.FileSystem.CurrentDirectory = InstLoc
                        If SelectDiff.FileName = "" Then ' If No File Was Selected
                            MsgBox("No Diff File Was Selected")
                            Return
                        Else
                            File.Copy(SelectDiff.FileName, "NandDiff.txt", True)
                        End If
                    End If
                    MakeCMD = New StreamWriter("CMD.bat", False)
                    MakeCMD.WriteLine("@echo off")
                    MakeCMD.WriteLine(NandCommand & ComPort & NandNumber & NandVerify & WriteMode & "CFW.bin" & DiffWriteFile & NandWriteOffset) 'Do Read Command in CMD Prompt
                    MakeCMD.WriteLine(NandCommand & ComPort & NandNumber & "release") 'Do Read Command in CMD Prompt
                    MakeCMD.WriteLine("pause")
                    MakeCMD.Close()
                    CMD = "CMD.bat"
                    Shell(CMD, vbNormalFocus, True)
                    If File.Exists("CFW.bin") = True Then
                        Try
                            File.Delete("CFW.bin")
                        Catch ex As Exception
                        End Try
                    End If
                    If File.Exists("NandDiff.txt") = True Then
                        Try
                            File.Delete("NandDiff.txt")
                        Catch ex As Exception
                        End Try
                    End If
                    If File.Exists("CMD.bat") = True Then
                        Try
                            File.Delete("CMD.bat")
                        Catch ex As Exception
                        End Try
                    End If
                    End If
            End If
        End If
        SelectFile.FileName = ""
    End Sub

    Private Sub NandJoinSplit_Click(sender As Object, e As EventArgs) Handles NandJoinSplit.Click
        Diagnostics.Process.Start(InstLoc & "\FlowRebuilder.exe").WaitForExit()
    End Sub

    Private Sub NandAdvancedCHK_CheckedChanged(sender As Object, e As EventArgs) Handles NandAdvancedCHK.CheckedChanged
        If NandAdvancedCHK.Checked = True Then
            NandOffset.Enabled = True
            NandLength.Enabled = True
            NandVerifyCHK.Enabled = True
        Else
            NandOffset.Enabled = False
            NandLength.Enabled = False
            NandVerifyCHK.Enabled = False
            NandVerifyCHK.Checked = False
            NandOffset.Text = ""
            NandLength.Text = ""
        End If
    End Sub
    Private Sub NandVerifyCHK_CheckedChanged(sender As Object, e As EventArgs) Handles NandVerifyCHK.CheckedChanged
        If NandVerifyCHK.Checked = True Then
            NandVerify = "v"
        Else
            NandVerify = ""
        End If
    End Sub
    Private Sub NandOffset_TextChanged(sender As Object, e As EventArgs) Handles NandOffset.TextChanged
        NandReadOffset = " " & NandOffset.Text & " " & NandLength.Text
        NandWriteOffset = " " & NandOffset.Text & " " & NandLength.Text
    End Sub
    Private Sub NandLength_TextChanged(sender As Object, e As EventArgs) Handles NandLength.TextChanged
        NandReadOffset = " " & NandOffset.Text & " " & NandLength.Text
        NandWriteOffset = " " & NandOffset.Text & " " & NandLength.Text
    End Sub
#End Region

#Region "Images Tab"
    Private Sub ImageSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ImageSelection.SelectedIndexChanged
        If ImageSelection.SelectedIndex = 0 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.DNY_001
            PicHight = 720
            PicWidth = 1280
        ElseIf ImageSelection.SelectedIndex = 1 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.DIA_001___DIA_002
            PicHight = 720
            PicWidth = 1280
        ElseIf ImageSelection.SelectedIndex = 2 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.JSD_001___SUR_001___JTP_001___KTE_001
            PicHight = 720
            PicWidth = 1280
        ElseIf ImageSelection.SelectedIndex = 3 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.VER_001
            PicHight = 720
            PicWidth = 1280
        ElseIf ImageSelection.SelectedIndex = 4 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.NOR_Clip
            PicHight = 720
            PicWidth = 600
        ElseIf ImageSelection.SelectedIndex = 5 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.Soft_QSB
            PicHight = 720
            PicWidth = 1200
        ElseIf ImageSelection.SelectedIndex = 6 Then
            PreviewImage.BackgroundImage = Downgrade_Tool.My.Resources.Resources.Nand_Clip
            PicHight = 720
            PicWidth = 1200
        End If
    End Sub
    Private Sub PreviewImage_Click(sender As Object, e As EventArgs) Handles PreviewImage.Click
        PictureViewer.Show()
    End Sub
#End Region

#Region "CMD Line Tab"
    Private Sub FilesLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilesLocation.Click
        Shell("explorer.exe " & InstLoc, vbNormalFocus)
    End Sub
    Private Sub RunAdvancedCMD_Click(sender As Object, e As EventArgs) Handles RunAdvancedCMD.Click
        Call RunCMD()
    End Sub
#Region "CMD Line Tab"
    Private Sub RunCMD()
        MakeCMD = New StreamWriter("CMD.bat", False)
        MakeCMD.WriteLine("@echo off")
        MakeCMD.WriteLine(AdvancedCommand.Text)
        MakeCMD.WriteLine("pause")
        MakeCMD.Close()
        CMD = "CMD.bat"
        Shell(CMD, vbNormalFocus, True)
        If File.Exists("CMD.bat") = True Then
            File.Delete("CMD.bat")
        Else
        End If
    End Sub
#End Region
#End Region

#Region "Settings Tab"
    Private Sub InstallPython_Click(sender As Object, e As EventArgs) Handles InstallPython.Click
        If File.Exists("python.msi") = True Then
            Try
                MakeCMD = New StreamWriter("CMD.bat", False)
                MakeCMD.WriteLine("@echo off")
                MakeCMD.WriteLine("start python.msi") 'Run Program
                MakeCMD.Close()
                CMD = "CMD.bat"
                Shell(CMD, vbHide, True)
                If File.Exists("CMD.bat") = True Then
                    Try
                        File.Delete("CMD.bat")
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub InstallpySerial_Click(sender As Object, e As EventArgs) Handles InstallpySerial.Click
        If File.Exists("pyserial.exe") = True Then
            Try
                MakeCMD = New StreamWriter("CMD.bat", False)
                MakeCMD.WriteLine("@echo off")
                MakeCMD.WriteLine("start pyserial.exe") 'Run Program
                MakeCMD.Close()
                CMD = "CMD.bat"
                Shell(CMD, vbHide, True)
                If File.Exists("CMD.bat") = True Then
                    Try
                        File.Delete("CMD.bat")
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub InstallTeensyDriver_Click(sender As Object, e As EventArgs) Handles InstallTeensyDriver.Click
        If File.Exists("driver.exe") = True Then
            Try
                MakeCMD = New StreamWriter("CMD.bat", False)
                MakeCMD.WriteLine("@echo off")
                MakeCMD.WriteLine("start driver.exe") 'Run Program
                MakeCMD.Close()
                CMD = "CMD.bat"
                Shell(CMD, vbHide, True)
                If File.Exists("CMD.bat") = True Then
                    Try
                        File.Delete("CMD.bat")
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub FlashTeensy2plusNOR_Click(sender As Object, e As EventArgs) Handles FlashTeensy2plusNOR.Click
        Dim message = MessageBox.Show("Connect Your Teensy Now -> Than Press The Little Button On It -> Then Click OK", "Press Button", MessageBoxButtons.OKCancel)
        If message = DialogResult.OK Then
            If File.Exists("teensy.exe") = True Then
                Try
                    MakeCMD = New StreamWriter("CMD.bat", False)
                    MakeCMD.WriteLine("teensy -mmcu=at90usb1286 -w NORway.hex") 'Run Program
                    MakeCMD.Close()
                    CMD = "CMD.bat"
                    Shell(CMD, vbNormalFocus, True)
                    If File.Exists("CMD.bat") = True Then
                        Try
                            File.Delete("CMD.bat")
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
    Private Sub FlashTeensy2plusNAND_Click(sender As Object, e As EventArgs) Handles FlashTeensy2plusNAND.Click
        Dim message = MessageBox.Show("Connect Your Teensy Now -> Than Press The Little Button On It -> Then Click OK", "Press Button", MessageBoxButtons.OKCancel)
        If message = DialogResult.OK Then
            If File.Exists("teensy.exe") = True Then
                Try
                    MakeCMD = New StreamWriter("CMD.bat", False)
                    MakeCMD.WriteLine("teensy -mmcu=at90usb1286 -w NANDway.hex") 'Run Program
                    MakeCMD.Close()
                    CMD = "CMD.bat"
                    Shell(CMD, vbNormalFocus, True)
                    If File.Exists("CMD.bat") = True Then
                        Try
                            File.Delete("CMD.bat")
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub DeviceMangerButton_Click(sender As Object, e As EventArgs) Handles DeviceMangerButton.Click
        Process.Start(System32Folder & "\devmgmt.msc")
    End Sub

    Private Sub ComPortDD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComPortDD.SelectedIndexChanged
        ComPort = My.Settings.ComPortDD
    End Sub
#End Region

#Region "Downloads Tab"
    Private Sub DownloadMinVerChk_Click(sender As Object, e As EventArgs) Handles DownloadMinVerChk.Click
        'Save  File Box
        Dim DownloadFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        DownloadFolderBrowser.ShowNewFolderButton = True
        DownloadFolderBrowser.ShowDialog()
        Downloader.Show()
        Downloader.SaveLocation = DownloadFolderBrowser.SelectedPath & "\MinVerChk"
        Directory.CreateDirectory(Downloader.SaveLocation)
        Downloader.DownloadingLBL.Text = "Downloading MinVerChk"
        Downloader.FileToDownload = "Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/Downloads/MinVerChk/PS3UPDAT.PUP"
        Downloader.StartButton.PerformClick()

        DownloadFolderBrowser.SelectedPath = ""
    End Sub

    Private Sub DownloadRogero450CFW_Click(sender As System.Object, e As System.EventArgs) Handles DownloadRogero450CFW.Click
        'Save  File Box
        Dim DownloadFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        DownloadFolderBrowser.ShowNewFolderButton = True
        DownloadFolderBrowser.ShowDialog()
        Downloader.Show()
        Downloader.SaveLocation = DownloadFolderBrowser.SelectedPath & "\Rogero 4.50 CFW"
        Directory.CreateDirectory(Downloader.SaveLocation)
        Downloader.DownloadingLBL.Text = "Downloading Rogero 4.50 CFW"
        Downloader.FileToDownload = "Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/Downloads/Rogero 4.50 CFW/PS3UPDAT.PUP"
        Downloader.StartButton.PerformClick()

        DownloadFolderBrowser.SelectedPath = ""
    End Sub

    Private Sub DownloadRogero446CFW_Click(sender As Object, e As EventArgs) Handles DownloadRogero446CFW.Click
        'Save  File Box
        Dim DownloadFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        DownloadFolderBrowser.ShowNewFolderButton = True
        DownloadFolderBrowser.ShowDialog()
        Downloader.Show()
        Downloader.SaveLocation = DownloadFolderBrowser.SelectedPath & "\Rogero 4.46 CFW"
        Directory.CreateDirectory(Downloader.SaveLocation)
        Downloader.DownloadingLBL.Text = "Downloading Rogero 4.46 CFW"
        Downloader.FileToDownload = "Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/Downloads/Rogero 4.46 CFW/PS3UPDAT.PUP"
        Downloader.StartButton.PerformClick()

        DownloadFolderBrowser.SelectedPath = ""
    End Sub

    Private Sub DownloadRogeroDowngradeto355_Click(sender As Object, e As EventArgs) Handles DownloadRogeroDowngradeto355.Click
        'Save  File Box
        Dim DownloadFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        DownloadFolderBrowser.ShowNewFolderButton = True
        DownloadFolderBrowser.ShowDialog()
        Downloader.Show()
        Downloader.SaveLocation = DownloadFolderBrowser.SelectedPath & "\Rogero Downgrade to 3.55"
        Directory.CreateDirectory(Downloader.SaveLocation)
        Downloader.DownloadingLBL.Text = "Downloading Rogero Downgrade to 3.55"
        Downloader.FileToDownload = "Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/Downloads/Rogero Downgrade to 3.55/PS3UPDAT.PUP"
        Downloader.StartButton.PerformClick()

        DownloadFolderBrowser.SelectedPath = ""
    End Sub

    Private Sub DownloadRogeroCEX355CFW_Click(sender As Object, e As EventArgs) Handles DownloadRogeroCEX355CFW.Click
        'Save  File Box
        Dim DownloadFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        DownloadFolderBrowser.ShowNewFolderButton = True
        DownloadFolderBrowser.ShowDialog()
        Downloader.Show()
        Downloader.SaveLocation = DownloadFolderBrowser.SelectedPath & "\Rogero CEX-3.55 CFW"
        Directory.CreateDirectory(Downloader.SaveLocation)
        Downloader.DownloadingLBL.Text = "Downloading Rogero CEX-3.55 CFW"
        Downloader.FileToDownload = "Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/Downloads/Rogero CEX-3.55 CFW/PS3UPDAT.PUP"
        Downloader.StartButton.PerformClick()

        DownloadFolderBrowser.SelectedPath = ""
    End Sub

    Private Sub DownloadToggleQA_Click(sender As Object, e As EventArgs) Handles DownloadToggleQA.Click
        'Save  File Box
        Dim DownloadFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        DownloadFolderBrowser.ShowNewFolderButton = True
        DownloadFolderBrowser.ShowDialog()
        Downloader.Show()
        Downloader.SaveLocation = DownloadFolderBrowser.SelectedPath
        Downloader.DownloadingLBL.Text = "Downloading Toggle_QA"
        Downloader.FileToDownload = "Http://coolshrimpmodz.com/HostedFiles/DowngradeTool/Downloads/Toggle_QA.pkg"
        Downloader.StartButton.PerformClick()

        DownloadFolderBrowser.SelectedPath = ""
    End Sub
#End Region

#Region "Other"
    Private Sub LogoImg_Click(sender As Object, e As EventArgs) Handles LogoImg.Click
        System.Diagnostics.Process.Start("http://coolshrimpmodz.com")    'link to Creator Site
    End Sub
#End Region
    
End Class