<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CrashHandler
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
        Me.crash_TextLog = New System.Windows.Forms.TextBox()
        Me.crash_Close = New System.Windows.Forms.Button()
        Me.crash_Text = New System.Windows.Forms.Label()
        Me.crash_Restart = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'crash_TextLog
        '
        Me.crash_TextLog.Location = New System.Drawing.Point(-2, -1)
        Me.crash_TextLog.Multiline = True
        Me.crash_TextLog.Name = "crash_TextLog"
        Me.crash_TextLog.Size = New System.Drawing.Size(623, 560)
        Me.crash_TextLog.TabIndex = 0
        Me.crash_TextLog.Text = "-- Server Checking Tool (by Narod) for TruckersMP has crashed --"
        '
        'crash_Close
        '
        Me.crash_Close.Location = New System.Drawing.Point(469, 560)
        Me.crash_Close.Name = "crash_Close"
        Me.crash_Close.Size = New System.Drawing.Size(152, 47)
        Me.crash_Close.TabIndex = 1
        Me.crash_Close.Text = "Quit"
        Me.crash_Close.UseVisualStyleBackColor = True
        '
        'crash_Text
        '
        Me.crash_Text.AutoSize = True
        Me.crash_Text.Location = New System.Drawing.Point(12, 562)
        Me.crash_Text.Name = "crash_Text"
        Me.crash_Text.Size = New System.Drawing.Size(295, 39)
        Me.crash_Text.TabIndex = 2
        Me.crash_Text.Text = "YART has crashed." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please send us this report through the forums using PasteBin" &
    "."
        '
        'crash_Restart
        '
        Me.crash_Restart.Location = New System.Drawing.Point(330, 560)
        Me.crash_Restart.Name = "crash_Restart"
        Me.crash_Restart.Size = New System.Drawing.Size(133, 47)
        Me.crash_Restart.TabIndex = 3
        Me.crash_Restart.Text = "Restart"
        Me.crash_Restart.UseVisualStyleBackColor = True
        '
        'CrashHandler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(621, 607)
        Me.Controls.Add(Me.crash_Restart)
        Me.Controls.Add(Me.crash_Text)
        Me.Controls.Add(Me.crash_Close)
        Me.Controls.Add(Me.crash_TextLog)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "CrashHandler"
        Me.ShowIcon = False
        Me.Text = "CrashHandler"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents crash_TextLog As TextBox
    Friend WithEvents crash_Close As Button
    Friend WithEvents crash_Text As Label
    Friend WithEvents crash_Restart As Button
End Class
