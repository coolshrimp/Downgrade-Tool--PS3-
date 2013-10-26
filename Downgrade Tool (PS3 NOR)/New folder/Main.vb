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
#End Region

Public Class Main
    Public MakeCMD As StreamWriter
    Dim ReadTimes As Integer
    Public CMD, InstLoc As String

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

            Dim blockSize As Integer = &H4200
            If Not ValidFileSize(inStream1.Length, blockSize) Then
                MessageBox.Show("Wrong file lengths!")
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

    Private Sub ReadButton_Click(sender As Object, e As EventArgs) Handles ReadButton.Click
        MakeCMD = New StreamWriter("CMD.bat", False)
        MakeCMD.WriteLine("@echo off")
        MakeCMD.WriteLine("NandPro " & "-r" & " Nand.bin") 'Do Read Command in CMD Prompt
        MakeCMD.WriteLine("pause")
        MakeCMD.Close()
        CMD = "CMD.bat"
        Shell(CMD, vbNormalFocus, True)
        If File.Exists("Nand.bin") = True Then
            'Save  File Box
            SaveFile.Title = "Select Save Location"
            SaveFile.FileName = "Nand"
            SaveFile.ShowDialog()
            My.Computer.FileSystem.CurrentDirectory = InstLoc
            File.Copy("Nand.bin", SaveFile.FileName, True)
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
            If File.Exists("Nand.Bin") = True Then
                File.Delete("Nand.Bin")
            End If

        Else
            MsgBox("Reading Error Try Again") 'Notify Error!!

        End If
        If File.Exists("CMD.bat") = True Then
            File.Delete("CMD.bat")
        End If
        SaveFile.FileName = ""
    End Sub


End Class