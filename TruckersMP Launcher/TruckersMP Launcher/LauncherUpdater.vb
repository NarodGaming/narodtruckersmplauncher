Imports System.IO
Imports System.Net
Imports System.Threading

Public Class LauncherUpdater
    Private _fadeOpacity As Single = 0
    Private oldImage As Bitmap
    Private newImage As Bitmap

    Private LogoFaded As Decimal = 0

    Private YartDate As String

    Private WithEvents wc As New WebClient

    Private Function FadeBitmap(ByVal bmp As Bitmap, ByVal opacity As Single) As Bitmap
        Dim bmp2 As New Bitmap(bmp.Width, bmp.Height, Imaging.PixelFormat.Format32bppArgb)
        opacity = Math.Max(0, Math.Min(opacity, 1.0F))
        Using ia As New Imaging.ImageAttributes
            Dim cm As New Imaging.ColorMatrix
            cm.Matrix33 = opacity
            ia.SetColorMatrix(cm)
            Dim destpoints() As PointF = {New Point(0, 0), New Point(bmp.Width, 0), New Point(0, bmp.Height)}
            Using g As Graphics = Graphics.FromImage(bmp2)
                g.DrawImage(bmp, destpoints,
          New RectangleF(Point.Empty, bmp.Size), GraphicsUnit.Pixel, ia)
            End Using
        End Using
        Return bmp2
    End Function

    Private Sub LauncherUpdater_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        yart_picture_logo.Image = FadeBitmap(My.Resources.truckersmp_logo, 0)
        logo_fadein.Start()

        AddHandler wc.DownloadFileCompleted, AddressOf WC_Downloaded_File

        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub logo_fadein_Tick(sender As Object, e As EventArgs) Handles logo_fadein.Tick
        If Not LogoFaded = 1 Then
            LogoFaded = LogoFaded + 0.02
            yart_picture_logo.Image = FadeBitmap(My.Resources.truckersmp_logo, LogoFaded)
        Else
            logo_fadein.Stop()

            Dim checkversion As WebClient = New WebClient() ' creates webclient for checking version
            checkversion.CachePolicy = New System.Net.Cache.RequestCachePolicy(Cache.RequestCacheLevel.NoCacheNoStore) ' to stop caching
            checkversion.Headers.Add("Cache-control", "no-cache")
            checkversion.Headers.Add("Cache-control", "no-store")
            checkversion.Headers.Add("pragma", "no-cache")
            checkversion.Headers.Add("Expries", "-1")
            Dim versionresponse As String = checkversion.DownloadString("https://narodgaming.ml/yart/currentver.txt?t=" + Date.Now.ToLocalTime) ' adds the date to prevent caching

            Dim YARTVersion As String = versionresponse.Split(" ")(0)
            YartDate = versionresponse.Split(" ")(1)

            yart_updating_text.Text = "YART is updating to version " + YARTVersion + "!"
            yart_updating_text.Visible = True

            update_progress.Style = ProgressBarStyle.Marquee
            update_progress.Visible = True

            download_worker.RunWorkerAsync()
        End If
    End Sub

    Private Sub download_worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles download_worker.DoWork

        Dim YartAddress As String = "https://narodgaming.ml/yart/downloads/" + YartDate + "/YART.exe"

        If File.Exists(Path.GetTempPath + "YART_NEW.exe") Then
            File.Delete(Path.GetTempPath + "YART_NEW.exe")
        End If

        wc.DownloadFileAsync(New Uri(YartAddress), Path.GetTempPath + "YART_NEW.exe")
    End Sub

    Private Sub WC_Downloaded_File(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        CreateRunScript()
    End Sub

    Private Function CreateRunScript()
        Using sw As StreamWriter = File.CreateText(Directory.GetCurrentDirectory + "\update.bat")
            sw.WriteLine("@echo off")
            sw.WriteLine("set oldfile=%*")
            sw.WriteLine("timeout 2 > NUL")
            sw.WriteLine("del %oldfile%")
            sw.WriteLine("move %temp%\YART_NEW.exe YART.exe")
            sw.WriteLine("start YART.exe")
        End Using

        Process.Start("update.bat ", Application.ExecutablePath)
        Application.Exit()
    End Function
End Class