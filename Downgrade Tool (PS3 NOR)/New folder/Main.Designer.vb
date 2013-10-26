<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ReadButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Nand1TextBox = New System.Windows.Forms.TextBox()
        Me.CompareNandButton = New System.Windows.Forms.Button()
        Me.CompareResultTextBox = New System.Windows.Forms.TextBox()
        Me.Nand2Button = New System.Windows.Forms.Button()
        Me.Nand1Button = New System.Windows.Forms.Button()
        Me.Nand2TextBox = New System.Windows.Forms.TextBox()
        Me.CompareNandProgress = New System.Windows.Forms.ProgressBar()
        Me.Nand2Lable = New System.Windows.Forms.Label()
        Me.Nand1Lable = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.DashVERGlitchSelection = New System.Windows.Forms.ComboBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.InstallFBGlitchButton = New System.Windows.Forms.Button()
        Me.WebBrowser = New System.Net.WebClient()
        Me.SaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.SelectFile = New System.Windows.Forms.OpenFileDialog()
        Me.WebClientLNK = New System.Net.WebClient()
        Me.WebClientVER = New System.Net.WebClient()
        Me.DownloadFolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ReadButton)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(260, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(150, 66)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Step 1 (Read Nand)"
        '
        'ReadButton
        '
        Me.ReadButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReadButton.ForeColor = System.Drawing.Color.Black
        Me.ReadButton.Location = New System.Drawing.Point(9, 19)
        Me.ReadButton.Name = "ReadButton"
        Me.ReadButton.Size = New System.Drawing.Size(135, 38)
        Me.ReadButton.TabIndex = 21
        Me.ReadButton.Text = "Read Nand"
        Me.ReadButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Nand1TextBox)
        Me.GroupBox2.Controls.Add(Me.CompareNandButton)
        Me.GroupBox2.Controls.Add(Me.CompareResultTextBox)
        Me.GroupBox2.Controls.Add(Me.Nand2Button)
        Me.GroupBox2.Controls.Add(Me.Nand1Button)
        Me.GroupBox2.Controls.Add(Me.Nand2TextBox)
        Me.GroupBox2.Controls.Add(Me.CompareNandProgress)
        Me.GroupBox2.Controls.Add(Me.Nand2Lable)
        Me.GroupBox2.Controls.Add(Me.Nand1Lable)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(12, 75)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(398, 274)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Step 2 (Compare Nands)"
        '
        'Nand1TextBox
        '
        Me.Nand1TextBox.Location = New System.Drawing.Point(15, 39)
        Me.Nand1TextBox.Name = "Nand1TextBox"
        Me.Nand1TextBox.Size = New System.Drawing.Size(336, 20)
        Me.Nand1TextBox.TabIndex = 0
        '
        'CompareNandButton
        '
        Me.CompareNandButton.Enabled = False
        Me.CompareNandButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompareNandButton.ForeColor = System.Drawing.Color.Black
        Me.CompareNandButton.Location = New System.Drawing.Point(310, 108)
        Me.CompareNandButton.Name = "CompareNandButton"
        Me.CompareNandButton.Size = New System.Drawing.Size(75, 23)
        Me.CompareNandButton.TabIndex = 7
        Me.CompareNandButton.Text = "Compare"
        Me.CompareNandButton.UseVisualStyleBackColor = True
        '
        'CompareResultTextBox
        '
        Me.CompareResultTextBox.Enabled = False
        Me.CompareResultTextBox.Location = New System.Drawing.Point(15, 137)
        Me.CompareResultTextBox.Multiline = True
        Me.CompareResultTextBox.Name = "CompareResultTextBox"
        Me.CompareResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.CompareResultTextBox.Size = New System.Drawing.Size(370, 117)
        Me.CompareResultTextBox.TabIndex = 3
        '
        'Nand2Button
        '
        Me.Nand2Button.ForeColor = System.Drawing.Color.Black
        Me.Nand2Button.Location = New System.Drawing.Point(357, 80)
        Me.Nand2Button.Name = "Nand2Button"
        Me.Nand2Button.Size = New System.Drawing.Size(28, 23)
        Me.Nand2Button.TabIndex = 9
        Me.Nand2Button.Text = "..."
        Me.Nand2Button.UseVisualStyleBackColor = True
        '
        'Nand1Button
        '
        Me.Nand1Button.ForeColor = System.Drawing.Color.Black
        Me.Nand1Button.Location = New System.Drawing.Point(357, 37)
        Me.Nand1Button.Name = "Nand1Button"
        Me.Nand1Button.Size = New System.Drawing.Size(28, 23)
        Me.Nand1Button.TabIndex = 4
        Me.Nand1Button.Text = "..."
        Me.Nand1Button.UseVisualStyleBackColor = True
        '
        'Nand2TextBox
        '
        Me.Nand2TextBox.Location = New System.Drawing.Point(15, 79)
        Me.Nand2TextBox.Name = "Nand2TextBox"
        Me.Nand2TextBox.Size = New System.Drawing.Size(336, 20)
        Me.Nand2TextBox.TabIndex = 1
        '
        'CompareNandProgress
        '
        Me.CompareNandProgress.Location = New System.Drawing.Point(15, 108)
        Me.CompareNandProgress.Name = "CompareNandProgress"
        Me.CompareNandProgress.Size = New System.Drawing.Size(289, 23)
        Me.CompareNandProgress.TabIndex = 11
        '
        'Nand2Lable
        '
        Me.Nand2Lable.AutoSize = True
        Me.Nand2Lable.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nand2Lable.ForeColor = System.Drawing.Color.White
        Me.Nand2Lable.Location = New System.Drawing.Point(12, 64)
        Me.Nand2Lable.Name = "Nand2Lable"
        Me.Nand2Lable.Size = New System.Drawing.Size(45, 13)
        Me.Nand2Lable.TabIndex = 14
        Me.Nand2Lable.Text = "Nand 2:"
        '
        'Nand1Lable
        '
        Me.Nand1Lable.AutoSize = True
        Me.Nand1Lable.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nand1Lable.ForeColor = System.Drawing.Color.White
        Me.Nand1Lable.Location = New System.Drawing.Point(12, 23)
        Me.Nand1Lable.Name = "Nand1Lable"
        Me.Nand1Lable.Size = New System.Drawing.Size(45, 13)
        Me.Nand1Lable.TabIndex = 13
        Me.Nand1Lable.Text = "Nand 1:"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.DashVERGlitchSelection)
        Me.GroupBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.ForeColor = System.Drawing.Color.White
        Me.GroupBox9.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(166, 42)
        Me.GroupBox9.TabIndex = 62
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Step 1 (Dash To Install)"
        '
        'DashVERGlitchSelection
        '
        Me.DashVERGlitchSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DashVERGlitchSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DashVERGlitchSelection.ForeColor = System.Drawing.Color.Black
        Me.DashVERGlitchSelection.FormattingEnabled = True
        Me.DashVERGlitchSelection.Items.AddRange(New Object() {"13599", "13604", "14699"})
        Me.DashVERGlitchSelection.Location = New System.Drawing.Point(12, 15)
        Me.DashVERGlitchSelection.Name = "DashVERGlitchSelection"
        Me.DashVERGlitchSelection.Size = New System.Drawing.Size(143, 21)
        Me.DashVERGlitchSelection.TabIndex = 60
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.InstallFBGlitchButton)
        Me.GroupBox10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.ForeColor = System.Drawing.Color.White
        Me.GroupBox10.Location = New System.Drawing.Point(1002, 12)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(236, 64)
        Me.GroupBox10.TabIndex = 63
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Step 1 (Write FreeBOOT to Console)"
        '
        'InstallFBGlitchButton
        '
        Me.InstallFBGlitchButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstallFBGlitchButton.ForeColor = System.Drawing.Color.Black
        Me.InstallFBGlitchButton.Location = New System.Drawing.Point(20, 17)
        Me.InstallFBGlitchButton.Name = "InstallFBGlitchButton"
        Me.InstallFBGlitchButton.Size = New System.Drawing.Size(194, 41)
        Me.InstallFBGlitchButton.TabIndex = 44
        Me.InstallFBGlitchButton.TabStop = False
        Me.InstallFBGlitchButton.Text = "Write FreeBOOT To Console"
        Me.InstallFBGlitchButton.UseVisualStyleBackColor = True
        '
        'WebBrowser
        '
        Me.WebBrowser.BaseAddress = ""
        Me.WebBrowser.CachePolicy = Nothing
        Me.WebBrowser.Credentials = Nothing
        Me.WebBrowser.Encoding = CType(resources.GetObject("WebBrowser.Encoding"), System.Text.Encoding)
        Me.WebBrowser.Headers = CType(resources.GetObject("WebBrowser.Headers"), System.Net.WebHeaderCollection)
        Me.WebBrowser.QueryString = CType(resources.GetObject("WebBrowser.QueryString"), System.Collections.Specialized.NameValueCollection)
        Me.WebBrowser.UseDefaultCredentials = False
        '
        'SaveFile
        '
        Me.SaveFile.DefaultExt = "bin"
        Me.SaveFile.Filter = ".Bin File|*.bin"
        '
        'SelectFile
        '
        Me.SelectFile.DefaultExt = "bin"
        Me.SelectFile.Filter = ".Bin File|*.bin"
        '
        'WebClientLNK
        '
        Me.WebClientLNK.BaseAddress = ""
        Me.WebClientLNK.CachePolicy = Nothing
        Me.WebClientLNK.Credentials = Nothing
        Me.WebClientLNK.Encoding = CType(resources.GetObject("WebClientLNK.Encoding"), System.Text.Encoding)
        Me.WebClientLNK.Headers = CType(resources.GetObject("WebClientLNK.Headers"), System.Net.WebHeaderCollection)
        Me.WebClientLNK.QueryString = CType(resources.GetObject("WebClientLNK.QueryString"), System.Collections.Specialized.NameValueCollection)
        Me.WebClientLNK.UseDefaultCredentials = False
        '
        'WebClientVER
        '
        Me.WebClientVER.BaseAddress = ""
        Me.WebClientVER.CachePolicy = Nothing
        Me.WebClientVER.Credentials = Nothing
        Me.WebClientVER.Encoding = CType(resources.GetObject("WebClientVER.Encoding"), System.Text.Encoding)
        Me.WebClientVER.Headers = CType(resources.GetObject("WebClientVER.Headers"), System.Net.WebHeaderCollection)
        Me.WebClientVER.QueryString = CType(resources.GetObject("WebClientVER.QueryString"), System.Collections.Specialized.NameValueCollection)
        Me.WebClientVER.UseDefaultCredentials = False
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1250, 608)
        Me.Controls.Add(Me.GroupBox10)
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ReadButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Nand1TextBox As System.Windows.Forms.TextBox
    Friend WithEvents CompareNandButton As System.Windows.Forms.Button
    Friend WithEvents CompareResultTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Nand2Button As System.Windows.Forms.Button
    Friend WithEvents Nand1Button As System.Windows.Forms.Button
    Friend WithEvents Nand2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents CompareNandProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents Nand2Lable As System.Windows.Forms.Label
    Friend WithEvents Nand1Lable As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents DashVERGlitchSelection As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents InstallFBGlitchButton As System.Windows.Forms.Button
    Friend WithEvents WebBrowser As System.Net.WebClient
    Friend WithEvents SaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SelectFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents WebClientLNK As System.Net.WebClient
    Friend WithEvents WebClientVER As System.Net.WebClient
    Friend WithEvents DownloadFolderBrowser As System.Windows.Forms.FolderBrowserDialog
End Class
