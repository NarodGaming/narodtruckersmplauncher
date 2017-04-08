<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rules_Popup
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
        Me.rules_important = New System.Windows.Forms.TextBox()
        Me.btn_accept = New System.Windows.Forms.Button()
        Me.btn_decline = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rules_important
        '
        Me.rules_important.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rules_important.Location = New System.Drawing.Point(0, 0)
        Me.rules_important.Multiline = True
        Me.rules_important.Name = "rules_important"
        Me.rules_important.ReadOnly = True
        Me.rules_important.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.rules_important.Size = New System.Drawing.Size(550, 550)
        Me.rules_important.TabIndex = 0
        '
        'btn_accept
        '
        Me.btn_accept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_accept.Location = New System.Drawing.Point(435, 0)
        Me.btn_accept.Name = "btn_accept"
        Me.btn_accept.Size = New System.Drawing.Size(115, 50)
        Me.btn_accept.TabIndex = 1
        Me.btn_accept.Text = "Accept"
        Me.btn_accept.UseVisualStyleBackColor = True
        '
        'btn_decline
        '
        Me.btn_decline.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_decline.Location = New System.Drawing.Point(313, 0)
        Me.btn_decline.Name = "btn_decline"
        Me.btn_decline.Size = New System.Drawing.Size(116, 50)
        Me.btn_decline.TabIndex = 2
        Me.btn_decline.Text = "Decline"
        Me.btn_decline.UseVisualStyleBackColor = True
        '
        'Rules_Popup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 550)
        Me.Controls.Add(Me.btn_decline)
        Me.Controls.Add(Me.btn_accept)
        Me.Controls.Add(Me.rules_important)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Rules_Popup"
        Me.ShowIcon = False
        Me.Text = "TruckersMP Rules"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Maroon
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rules_important As TextBox
    Friend WithEvents btn_accept As Button
    Friend WithEvents btn_decline As Button
End Class
