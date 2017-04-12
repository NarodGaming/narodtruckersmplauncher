<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
        Me.yart_pic_background = New System.Windows.Forms.PictureBox()
        Me.yart_text_label1 = New System.Windows.Forms.Label()
        Me.yart_version_label = New System.Windows.Forms.Label()
        Me.show_splash_timer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.yart_pic_background, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'yart_pic_background
        '
        Me.yart_pic_background.Dock = System.Windows.Forms.DockStyle.Fill
        Me.yart_pic_background.Image = Global.YART.My.Resources.Resources.truckersmp_logo
        Me.yart_pic_background.Location = New System.Drawing.Point(0, 0)
        Me.yart_pic_background.Name = "yart_pic_background"
        Me.yart_pic_background.Size = New System.Drawing.Size(462, 264)
        Me.yart_pic_background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.yart_pic_background.TabIndex = 0
        Me.yart_pic_background.TabStop = False
        '
        'yart_text_label1
        '
        Me.yart_text_label1.AutoSize = True
        Me.yart_text_label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yart_text_label1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.yart_text_label1.Location = New System.Drawing.Point(30, 9)
        Me.yart_text_label1.Name = "yart_text_label1"
        Me.yart_text_label1.Size = New System.Drawing.Size(395, 20)
        Me.yart_text_label1.TabIndex = 1
        Me.yart_text_label1.Text = "YART (Yet Another TruckersMP Launcher) is starting..."
        '
        'yart_version_label
        '
        Me.yart_version_label.AutoSize = True
        Me.yart_version_label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yart_version_label.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.yart_version_label.Location = New System.Drawing.Point(12, 242)
        Me.yart_version_label.Name = "yart_version_label"
        Me.yart_version_label.Size = New System.Drawing.Size(45, 13)
        Me.yart_version_label.TabIndex = 2
        Me.yart_version_label.Text = "Version "
        '
        'show_splash_timer
        '
        Me.show_splash_timer.Interval = 1
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(462, 264)
        Me.Controls.Add(Me.yart_version_label)
        Me.Controls.Add(Me.yart_text_label1)
        Me.Controls.Add(Me.yart_pic_background)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SplashScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SplashScreen"
        CType(Me.yart_pic_background, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents yart_pic_background As PictureBox
    Friend WithEvents yart_text_label1 As Label
    Friend WithEvents yart_version_label As Label
    Friend WithEvents show_splash_timer As Timer
End Class
