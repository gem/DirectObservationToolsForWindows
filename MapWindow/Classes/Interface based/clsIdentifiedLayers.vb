'********************************************************************************************************
'File Name: clsIdentifiedlayers.vb
'Description: Public class on the plugin interface for getting information about layers that were clicked.   
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

Public Class IdentifiedLayers
    Implements Interfaces.IdentifiedLayers

    Public Sub New()
        MyBase.New()
    End Sub
    Private m_Layers As New Hashtable

    Friend Sub Add(ByVal Item As MapWindow.IdentifiedShapes, ByVal hLyr As Integer)
        If Item Is Nothing Then Exit Sub
        If (m_Layers.ContainsKey(hLyr)) Then
            Dim t As IdentifiedShapes
            t = CType(m_Layers(hLyr), MapWindow.IdentifiedShapes)
            Dim i As Integer
            For i = 0 To Item.Count - 1
                t.Add(Item.Item(i))
            Next
        Else
            m_Layers.Add(hLyr, Item)
        End If
    End Sub

    Public ReadOnly Property Count() As Integer Implements Interfaces.IdentifiedLayers.Count
        Get
            Count = m_Layers.Count
        End Get
    End Property

    <CLSCompliant(False)> _
    Public ReadOnly Property Item(ByVal LayerHandle As Integer) As Interfaces.IdentifiedShapes Implements Interfaces.IdentifiedLayers.Item
        Get
            If m_Layers.ContainsKey(LayerHandle) Then
                Return CType(m_Layers(LayerHandle), MapWindow.Interfaces.IdentifiedShapes)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Friend Sub Remove(ByVal LayerHandle As Integer)
        If m_Layers.ContainsKey(LayerHandle) Then m_Layers.Remove(LayerHandle)
    End Sub

End Class
