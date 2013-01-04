'********************************************************************************************************
'File Name: clsIdentifiedShapes.vb
'Description: Public class on the plugin interface for getting information about shapes that were clicked.   
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
Public Class IdentifiedShapes
    Implements Interfaces.IdentifiedShapes

    Private m_Shapes As New ArrayList

    Friend Sub Add(ByVal Item As Integer)
        m_Shapes.Add(Item)
    End Sub

    Public ReadOnly Property Count() As Integer Implements Interfaces.IdentifiedShapes.Count
        Get
            Count = m_Shapes.Count
        End Get
    End Property

    Public ReadOnly Property Item(ByVal Index As Integer) As Integer Implements Interfaces.IdentifiedShapes.Item
        'Suggested fix by tvetter for bad indexes -- see
        'http://www.mapwindow.org/phorum/read.php?5,4720,4720#msg-4720
        Get
            Item = -1
            If Index < 0 Or Index > m_Shapes.Count - 1 Then Exit Property
            Item = CInt(m_Shapes(Index))
        End Get
    End Property

    Friend Sub Remove(ByVal Index As Integer)
        If Index < 0 Or Index > m_Shapes.Count - 1 Then Exit Sub
        m_Shapes.RemoveAt(Index)
    End Sub
End Class


