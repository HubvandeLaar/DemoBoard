<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParameters
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParameters))
        Me.cmbLanguage = New System.Windows.Forms.ComboBox()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.lblLessonsFolder = New System.Windows.Forms.Label()
        Me.txtLessonsFolder = New System.Windows.Forms.TextBox()
        Me.dlgLessonsFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.cmdLessonsFolder = New System.Windows.Forms.Button()
        Me.picSchakelaar = New System.Windows.Forms.PictureBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        CType(Me.picSchakelaar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbLanguage
        '
        Me.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLanguage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLanguage.FormattingEnabled = True
        Me.cmbLanguage.Items.AddRange(New Object() {"a", "Nederlands"})
        Me.cmbLanguage.Location = New System.Drawing.Point(220, 114)
        Me.cmbLanguage.Name = "cmbLanguage"
        Me.cmbLanguage.Size = New System.Drawing.Size(147, 24)
        Me.cmbLanguage.TabIndex = 0
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLanguage.Location = New System.Drawing.Point(33, 117)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(69, 16)
        Me.lblLanguage.TabIndex = 1
        Me.lblLanguage.Text = "Language"
        '
        'lblLessonsFolder
        '
        Me.lblLessonsFolder.AutoSize = True
        Me.lblLessonsFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLessonsFolder.Location = New System.Drawing.Point(33, 167)
        Me.lblLessonsFolder.Name = "lblLessonsFolder"
        Me.lblLessonsFolder.Size = New System.Drawing.Size(154, 16)
        Me.lblLessonsFolder.TabIndex = 2
        Me.lblLessonsFolder.Text = "Folder for chess lessons"
        '
        'txtLessonsFolder
        '
        Me.txtLessonsFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLessonsFolder.Location = New System.Drawing.Point(220, 164)
        Me.txtLessonsFolder.Name = "txtLessonsFolder"
        Me.txtLessonsFolder.Size = New System.Drawing.Size(330, 22)
        Me.txtLessonsFolder.TabIndex = 3
        '
        'dlgLessonsFolder
        '
        Me.dlgLessonsFolder.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'cmdLessonsFolder
        '
        Me.cmdLessonsFolder.Font = New System.Drawing.Font("Wingdings 3", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.cmdLessonsFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdLessonsFolder.Location = New System.Drawing.Point(529, 165)
        Me.cmdLessonsFolder.Name = "cmdLessonsFolder"
        Me.cmdLessonsFolder.Size = New System.Drawing.Size(20, 20)
        Me.cmdLessonsFolder.TabIndex = 5
        Me.cmdLessonsFolder.Text = "q"
        Me.cmdLessonsFolder.UseVisualStyleBackColor = True
        '
        'picSchakelaar
        '
        Me.picSchakelaar.BackColor = System.Drawing.SystemColors.Window
        Me.picSchakelaar.ErrorImage = Nothing
        Me.picSchakelaar.Image = CType(resources.GetObject("picSchakelaar.Image"), System.Drawing.Image)
        Me.picSchakelaar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.picSchakelaar.InitialImage = Nothing
        Me.picSchakelaar.Location = New System.Drawing.Point(12, 12)
        Me.picSchakelaar.Name = "picSchakelaar"
        Me.picSchakelaar.Size = New System.Drawing.Size(75, 75)
        Me.picSchakelaar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picSchakelaar.TabIndex = 19
        Me.picSchakelaar.TabStop = False
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(475, 214)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 21
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'frmParameters
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 255)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.picSchakelaar)
        Me.Controls.Add(Me.cmdLessonsFolder)
        Me.Controls.Add(Me.txtLessonsFolder)
        Me.Controls.Add(Me.lblLessonsFolder)
        Me.Controls.Add(Me.lblLanguage)
        Me.Controls.Add(Me.cmbLanguage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParameters"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "DemoBoard Parameters"
        CType(Me.picSchakelaar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbLanguage As ComboBox
    Friend WithEvents lblLanguage As Label
    Friend WithEvents lblLessonsFolder As Label
    Friend WithEvents txtLessonsFolder As TextBox
    Friend WithEvents dlgLessonsFolder As FolderBrowserDialog
    Friend WithEvents cmdLessonsFolder As Button
    Friend WithEvents picSchakelaar As PictureBox
    Friend WithEvents cmdOK As Button
End Class
