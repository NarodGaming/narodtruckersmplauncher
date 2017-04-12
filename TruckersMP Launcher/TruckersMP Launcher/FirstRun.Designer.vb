<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FirstRun
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
        Me.yart_introduction_lbl = New System.Windows.Forms.Label()
        Me.zoom_timer = New System.Windows.Forms.Timer(Me.components)
        Me.yart_instructions_lbl = New System.Windows.Forms.Label()
        Me.yart_instructions2_lbl = New System.Windows.Forms.Label()
        Me.yart_id = New System.Windows.Forms.TextBox()
        Me.yart_id_status = New System.Windows.Forms.PictureBox()
        Me.yart_leave_button = New System.Windows.Forms.Button()
        Me.yart_instructions_tip = New System.Windows.Forms.ToolTip(Me.components)
        Me.yart_link_lbl = New System.Windows.Forms.LinkLabel()
        Me.yart_tooltip_help = New System.Windows.Forms.Label()
        CType(Me.yart_id_status, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'yart_introduction_lbl
        '
        Me.yart_introduction_lbl.AutoSize = True
        Me.yart_introduction_lbl.Location = New System.Drawing.Point(551, 9)
        Me.yart_introduction_lbl.Name = "yart_introduction_lbl"
        Me.yart_introduction_lbl.Size = New System.Drawing.Size(367, 13)
        Me.yart_introduction_lbl.TabIndex = 0
        Me.yart_introduction_lbl.Text = "Welcome to Yet Another TruckersMP Launcher, otherwise known as YART."
        '
        'zoom_timer
        '
        Me.zoom_timer.Interval = 10
        '
        'yart_instructions_lbl
        '
        Me.yart_instructions_lbl.AutoSize = True
        Me.yart_instructions_lbl.Location = New System.Drawing.Point(12, 50)
        Me.yart_instructions_lbl.Name = "yart_instructions_lbl"
        Me.yart_instructions_lbl.Size = New System.Drawing.Size(173, 26)
        Me.yart_instructions_lbl.TabIndex = 1
        Me.yart_instructions_lbl.Text = "We only need one thing from you," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "your TruckersMP ID, or SteamID64"
        Me.yart_instructions_lbl.Visible = False
        '
        'yart_instructions2_lbl
        '
        Me.yart_instructions2_lbl.AutoSize = True
        Me.yart_instructions2_lbl.Location = New System.Drawing.Point(12, 142)
        Me.yart_instructions2_lbl.Name = "yart_instructions2_lbl"
        Me.yart_instructions2_lbl.Size = New System.Drawing.Size(152, 13)
        Me.yart_instructions2_lbl.TabIndex = 2
        Me.yart_instructions2_lbl.Text = "Enter your ID (either one) here:"
        Me.yart_instructions2_lbl.Visible = False
        '
        'yart_id
        '
        Me.yart_id.Location = New System.Drawing.Point(170, 139)
        Me.yart_id.Name = "yart_id"
        Me.yart_id.Size = New System.Drawing.Size(289, 20)
        Me.yart_id.TabIndex = 3
        Me.yart_id.Visible = False
        '
        'yart_id_status
        '
        Me.yart_id_status.Location = New System.Drawing.Point(465, 132)
        Me.yart_id_status.Name = "yart_id_status"
        Me.yart_id_status.Size = New System.Drawing.Size(39, 35)
        Me.yart_id_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.yart_id_status.TabIndex = 4
        Me.yart_id_status.TabStop = False
        Me.yart_id_status.Visible = False
        '
        'yart_leave_button
        '
        Me.yart_leave_button.Location = New System.Drawing.Point(210, 308)
        Me.yart_leave_button.Name = "yart_leave_button"
        Me.yart_leave_button.Size = New System.Drawing.Size(137, 61)
        Me.yart_leave_button.TabIndex = 5
        Me.yart_leave_button.Text = "Save and open YART!"
        Me.yart_leave_button.UseVisualStyleBackColor = True
        Me.yart_leave_button.Visible = False
        '
        'yart_link_lbl
        '
        Me.yart_link_lbl.AutoSize = True
        Me.yart_link_lbl.Location = New System.Drawing.Point(226, 57)
        Me.yart_link_lbl.Name = "yart_link_lbl"
        Me.yart_link_lbl.Size = New System.Drawing.Size(95, 13)
        Me.yart_link_lbl.TabIndex = 6
        Me.yart_link_lbl.TabStop = True
        Me.yart_link_lbl.Text = "SteamID64 Finder."
        Me.yart_link_lbl.Visible = False
        '
        'yart_tooltip_help
        '
        Me.yart_tooltip_help.AutoSize = True
        Me.yart_tooltip_help.ForeColor = System.Drawing.Color.Blue
        Me.yart_tooltip_help.Location = New System.Drawing.Point(184, 63)
        Me.yart_tooltip_help.Name = "yart_tooltip_help"
        Me.yart_tooltip_help.Size = New System.Drawing.Size(19, 13)
        Me.yart_tooltip_help.TabIndex = 7
        Me.yart_tooltip_help.Text = "(?)"
        '
        'FirstRun
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 408)
        Me.Controls.Add(Me.yart_tooltip_help)
        Me.Controls.Add(Me.yart_link_lbl)
        Me.Controls.Add(Me.yart_leave_button)
        Me.Controls.Add(Me.yart_id_status)
        Me.Controls.Add(Me.yart_id)
        Me.Controls.Add(Me.yart_instructions2_lbl)
        Me.Controls.Add(Me.yart_instructions_lbl)
        Me.Controls.Add(Me.yart_introduction_lbl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FirstRun"
        Me.ShowIcon = False
        Me.Text = "YART - First Time Setup"
        Me.yart_instructions_tip.SetToolTip(Me, "Your TruckersMP ID or SteamID64 can be used in YART.")
        CType(Me.yart_id_status, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents yart_introduction_lbl As Label
    Friend WithEvents zoom_timer As Timer
    Friend WithEvents yart_instructions_lbl As Label
    Friend WithEvents yart_instructions2_lbl As Label
    Friend WithEvents yart_id As TextBox
    Friend WithEvents yart_id_status As PictureBox
    Friend WithEvents yart_leave_button As Button
    Friend WithEvents yart_instructions_tip As ToolTip
    Friend WithEvents yart_link_lbl As LinkLabel
    Friend WithEvents yart_tooltip_help As Label
End Class
