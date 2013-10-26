<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Downloader
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Downloader))
        Me.DownloadingLBL = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ProgBar = New System.Windows.Forms.ProgressBar()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'DownloadingLBL
        '
        Me.DownloadingLBL.Location = New System.Drawing.Point(2, 4)
        Me.DownloadingLBL.Name = "DownloadingLBL"
        Me.DownloadingLBL.Size = New System.Drawing.Size(379, 16)
        Me.DownloadingLBL.TabIndex = 19
        Me.DownloadingLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblProgress
        '
        Me.lblProgress.Location = New System.Drawing.Point(2, 51)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(272, 16)
        Me.lblProgress.TabIndex = 18
        Me.lblProgress.Text = "#Progress"
        '
        'ProgBar
        '
        Me.ProgBar.Location = New System.Drawing.Point(5, 23)
        Me.ProgBar.Name = "ProgBar"
        Me.ProgBar.Size = New System.Drawing.Size(376, 16)
        Me.ProgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgBar.TabIndex = 16
        '
        'StartButton
        '
        Me.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.StartButton.Location = New System.Drawing.Point(202, 80)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(72, 24)
        Me.StartButton.TabIndex = 20
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdClose.Location = New System.Drawing.Point(309, 45)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(72, 24)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "Close"
        '
        'Downloader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(386, 74)
        Me.Controls.Add(Me.DownloadingLBL)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ProgBar)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.cmdClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Downloader"
        Me.Text = "Downloader"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DownloadingLBL As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ProgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
End Class
