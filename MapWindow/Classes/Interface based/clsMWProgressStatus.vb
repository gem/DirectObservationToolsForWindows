Public Class MWProgressStatus
    Implements MapWinUtility.IProgressStatus

    Private m_StatusPanel As System.Windows.Forms.ToolStripStatusLabel
    Private m_OrigCursor As Windows.Forms.Cursor

    Public Sub New()
        'm_StatusPanel = frmMain.StatusBarPanelStatus
    End Sub

    ''' <summary>
    ''' Log the progress of a long-running task
    ''' </summary>
    ''' <param name="CurrentPosition">Current position/item of task</param>
    ''' <param name="LastPosition">Final position/item of task</param>
    ''' <remarks>
    ''' A final call when the task is done with aCurrent = aLast 
    ''' indicates completion and should clear the progress display.
    ''' </remarks>
    Public Sub Progress(ByVal CurrentPosition As Integer, ByVal LastPosition As Integer) Implements MapWinUtility.IProgressStatus.Progress
        If CurrentPosition >= LastPosition Then 'Reached end, stop showing progress bar
            frmMain.StatusBar.ProgressBarValue = LastPosition
            frmMain.StatusBar.ShowProgressBar = False
            If m_OrigCursor IsNot Nothing Then
                frmMain.Cursor = m_OrigCursor
            Else
                frmMain.Cursor = Cursors.Default
            End If
        Else
            Try
                If Not frmMain.StatusBar.ShowProgressBar OrElse m_OrigCursor Is Nothing Then
                    m_OrigCursor = frmMain.Cursor 'Save cursor that was in use before progress started
                    frmMain.StatusBar.ShowProgressBar = True
                End If
                frmMain.StatusBar.ProgressBarValue = 100 * CDbl(CurrentPosition) / LastPosition
                Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Catch ex As Exception 'Ignore exception if we can't set ProgressBarValue
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Update the current status message
    ''' </summary>
    ''' <param name="StatusMessage">Description of current processing status</param>
    Public Sub Status(ByVal StatusMessage As String) Implements MapWinUtility.IProgressStatus.Status
        frmMain.m_StatusBar.ShowMessage(StatusMessage)
    End Sub
End Class
