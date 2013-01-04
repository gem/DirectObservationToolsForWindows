'********************************************************************************************************
'File Name: clsGridUtils.vb
'Description: Friend class used for grid management.  
'********************************************************************************************************
'The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
'you may not use this file except in compliance with the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/ 
'Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
'ANY KIND, either express or implied. See the License for the specificlanguage governing rights and 
'limitations under the License. 
'
'The Original Code is MapWindow Open Source. 
'
'The Initial Developer of this version of the Original Code is Daniel P. Ames using portions created by 
'Utah State University and the Idaho National Engineering and Environmental Lab that were released as 
'public domain in March 2004.  
'
'Contributor(s): (Open source contributors should list themselves and their modifications here). 
'1/31/2005 - No change from the public domain version.
'********************************************************************************************************
Imports System.Threading

Friend Class GridUtils
    Private m_Ready As Boolean
    Private Shared m_Grid As MapWinGIS.Grid
    Private m_Thread As Thread

    Private Sub InitThread()
        Try
            m_Grid = New MapWinGIS.Grid
        Catch ex As Exception
            MapWinUtility.Logger.Msg(ex.ToString)
        Finally
            m_Ready = True
        End Try
    End Sub

    Private Sub StartThread()
        'Due to funny conflicts with ESRI's grid stuff, we have to create the grid in a new thread 
        m_Ready = False
        m_Thread = New Thread(New ThreadStart(AddressOf InitThread))
        m_Thread.Start()

        While (m_Ready = False)
            Windows.Forms.Application.DoEvents()
            'System.Threading.Thread.Sleep(50)
            'The above line seems to make it hang if running from the IDE - but creating a grid is a quick operation, eating the CPU for a few ns won't hurt
        End While
    End Sub

    Private Sub StopThread()
        m_Thread.Abort()
        m_Thread = Nothing
    End Sub

    Public Function CreateSafeGrid() As MapWinGIS.Grid
        Try
            StartThread()
            StopThread()
            MapWinUtility.Logger.Msg(m_Thread.ThreadState.ToString())

        Catch ex As System.Exception
            ShowError(ex)
        End Try
        Return m_Grid
    End Function

    Public Function GridCdlgFilter() As String
        StartThread()
        StopThread()
        Return m_Grid.CdlgFilter
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
