Public Class SplashScreen
    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        yart_version_label.Text = Application.ProductVersion + "-Hotfix2"

        show_splash_timer.Start()
    End Sub

    Private Sub show_splash_timer_Tick(sender As Object, e As EventArgs) Handles show_splash_timer.Tick
        Form1.Show()
        show_splash_timer.Stop()
    End Sub
End Class