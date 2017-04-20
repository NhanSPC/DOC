
Imports pbs.Helper
Imports pbs.Helper.Interfaces
Imports System.Data
Imports Csla
Imports Csla.Data
Imports Csla.Validation



<Serializable()> _
Public Class DPROInfo
    Inherits Csla.ReadOnlyBase(Of DPROInfo)
    Implements IComparable
    Implements IInfo
    Implements IDocLink
    'Implements IInfoStatus


#Region " Business Properties and Methods "


    Private _lineNo As Integer
    Public ReadOnly Property LineNo() As Integer
        Get
            Return _lineNo
        End Get
    End Property

    Private _clientDocId As pbs.Helper.SmartInt32 = New pbs.Helper.SmartInt32(0)
    Public ReadOnly Property ClientDocId() As String
        Get
            Return _clientDocId.Text
        End Get
    End Property

    Private _processingDate As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public ReadOnly Property ProcessingDate() As String
        Get
            Return _processingDate.DateViewFormat
        End Get
    End Property

    Private _processingOperation As String = String.Empty
    Public ReadOnly Property ProcessingOperation() As String
        Get
            Return _processingOperation
        End Get
    End Property

    Private _processingBy As String = String.Empty
    Public ReadOnly Property ProcessingBy() As String
        Get
            Return _processingBy
        End Get
    End Property

    Private _status As String = String.Empty
    Public ReadOnly Property Status() As String
        Get
            Return _status
        End Get
    End Property

    Private _comment As String = String.Empty
    Public ReadOnly Property Comment() As String
        Get
            Return _comment
        End Get
    End Property

    Private _updated As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public ReadOnly Property Updated() As String
        Get
            Return _updated.DateViewFormat
        End Get
    End Property

    'Get ID
    Protected Overrides Function GetIdValue() As Object
        Return _lineNo
    End Function

    'IComparable
    Public Function CompareTo(ByVal IDObject) As Integer Implements System.IComparable.CompareTo
        Dim ID = IDObject.ToString
        Dim pLineNo As Integer = ID.Trim.ToInteger
        If _lineNo < pLineNo Then Return -1
        If _lineNo > pLineNo Then Return 1
        Return 0
    End Function

    Public ReadOnly Property Code As String Implements IInfo.Code
        Get
            Return _lineNo
        End Get
    End Property

    Public ReadOnly Property LookUp As String Implements IInfo.LookUp
        Get
            Return _lineNo
        End Get
    End Property

    Public ReadOnly Property Description As String Implements IInfo.Description
        Get
            Return _lineNo
        End Get
    End Property


    Public Sub InvalidateCache() Implements IInfo.InvalidateCache
        DPROInfoList.InvalidateCache()
    End Sub


#End Region 'Business Properties and Methods

#Region " Factory Methods "

    Friend Shared Function GetDPROInfo(ByVal dr As SafeDataReader) As DPROInfo
        Return New DPROInfo(dr)
    End Function

    Friend Shared Function EmptyDPROInfo(Optional ByVal pLineNo As String = "") As DPROInfo
        Dim info As DPROInfo = New DPROInfo
        With info
            ._lineNo = pLineNo.ToInteger

        End With
        Return info
    End Function

    Private Sub New(ByVal dr As SafeDataReader)
        _lineNo = dr.GetInt32("LINE_NO")
        _clientDocId.Text = dr.GetInt32("CLIENT_DOC_ID")
        _processingDate.Text = dr.GetDateTime("PROCESSING_DATE")
        _processingOperation = dr.GetString("PROCESSING_OPERATION").TrimEnd
        _processingBy = dr.GetString("PROCESSING_BY").TrimEnd
        _status = dr.GetString("STATUS").TrimEnd
        _comment = dr.GetString("COMMENT").TrimEnd
        _updated.Text = dr.GetDateTime("UPDATED")

    End Sub

    Private Sub New()
    End Sub


#End Region ' Factory Methods

#Region "IDoclink"
    Public Function Get_DOL_Reference() As String Implements IDocLink.Get_DOL_Reference
        Return String.Format("{0}#{1}", Get_TransType, _lineNo)
    End Function

    Public Function Get_TransType() As String Implements IDocLink.Get_TransType
        Return Me.GetType.ToClassSchemaName.Leaf
    End Function
#End Region

End Class

