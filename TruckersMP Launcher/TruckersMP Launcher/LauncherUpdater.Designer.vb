<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LauncherUpdater
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
        Me.components = New System.ComponentModel.Container()
        Me.yart_picture_logo = New System.Windows.Forms.PictureBox()
        Me.yart_pic_back = New System.Windows.Forms.PictureBox()
        Me.logo_fadein = New System.Windows.Forms.Timer(Me.components)
        Me.update_progress = New System.Windows.Forms.ProgressBar()
        Me.yart_updating_text = New System.Windows.Forms.Label()
        Me.download_worker = New System.ComponentModel.BackgroundWorker()
        CType(Me.yart_picture_logo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.yart_pic_back, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'yart_picture_logo
        '
        Me.yart_picture_logo.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.yart_picture_logo.Image = Global.YART.My.Resources.Resources.truckersmp_logo
        Me.yart_picture_logo.Location = New System.Drawing.Point(139, 12)
        Me.yart_picture_logo.Name = "yart_picture_logo"
        Me.yart_picture_logo.Size = New System.Drawing.Size(423, 96)
        Me.yart_picture_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.yart_picture_logo.TabIndex = 1
        Me.yart_picture_logo.TabStop = False
        '
        'yart_pic_back
        '
        Me.yart_pic_back.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.yart_pic_back.Dock = System.Windows.Forms.DockStyle.Fill
        Me.yart_pic_back.Location = New System.Drawing.Point(0, 0)
        Me.yart_pic_back.Name = "yart_pic_back"
        Me.yart_pic_back.Size = New System.Drawing.Size(706, 368)
        Me.yart_pic_back.TabIndex = 0
        Me.yart_pic_back.TabStop = False
        '
        'logo_fadein
        '
        Me.logo_fadein.Interval = 5
        '
        'update_progress
        '
        Me.update_progress.Location = New System.Drawing.Point(12, 322)
        Me.update_progress.Name = "update_progress"
        Me.update_progress.Size = New System.Drawing.Size(682, 34)
        Me.update_progress.TabIndex = 3
        Me.update_progress.Visible = False
        '
        'yart_updating_text
        '
        Me.yart_updating_text.AutoSize = True
        Me.yart_updating_text.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.yart_updating_text.ForeColor = System.Drawing.SystemColors.Control
        Me.yart_updating_text.Location = New System.Drawing.Point(12, 297)
        Me.yart_updating_text.Name = "yart_updating_text"
        Me.yart_updating_text.Size = New System.Drawing.Size(145, 13)
        Me.yart_updating_text.TabIndex = 4
        Me.yart_updating_text.Text = "YART is updating to version: "
        Me.yart_updating_text.Visible = False
        '
        'download_worker
        '
        '
        'LauncherUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(706, 368)
        Me.Controls.Add(Me.yart_updating_text)
        Me.Controls.Add(Me.update_progress)
        Me.Controls.Add(Me.yart_picture_logo)
        Me.Controls.Add(Me.yart_pic_back)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LauncherUpdater"
        Me.Text = "LauncherUpdater"
        CType(Me.yart_picture_logo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.yart_pic_back, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents yart_pic_back As PictureBox
    Friend WithEvents yart_picture_logo As PictureBox
    Friend WithEvents logo_fadein As Timer
    Friend WithEvents update_progress As ProgressBar
    Friend WithEvents yart_updating_text As Label
    Friend WithEvents download_worker As System.ComponentModel.BackgroundWorker
End Class
