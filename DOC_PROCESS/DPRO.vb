Imports pbs.Helper
Imports System.Data
Imports System.Data.SqlClient
Imports Csla
Imports Csla.Data
Imports Csla.Validation
Imports pbs.BO.DataAnnotations
Imports pbs.BO.Script
Imports pbs.BO.BusinessRules




<Serializable()> _
Public Class DPRO
    Inherits Csla.BusinessBase(Of DPRO)
    Implements Interfaces.IGenPartObject
    Implements IComparable
    Implements IDocLink



#Region "Property Changed"
    Protected Overrides Sub OnDeserialized(context As Runtime.Serialization.StreamingContext)
        MyBase.OnDeserialized(context)
        AddHandler Me.PropertyChanged, AddressOf BO_PropertyChanged
    End Sub

    Private Sub BO_PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs) Handles Me.PropertyChanged
        'Select Case e.PropertyName

        '    Case "OrderType"
        '        If Not Me.GetOrderTypeInfo.ManualRef Then
        '            Me._orderNo = POH.AutoReference
        '        End If

        '    Case "OrderDate"
        '        If String.IsNullOrEmpty(Me.OrderPrd) Then Me._orderPrd.Text = Me._orderDate.Date.ToSunPeriod

        '    Case "SuppCode"
        '        For Each line In Lines
        '            line._suppCode = Me.SuppCode
        '        Next

        '    Case "ConvCode"
        '        If String.IsNullOrEmpty(Me.ConvCode) Then
        '            _convRate.Float = 0
        '        Else
        '            Dim conv = pbs.BO.LA.CVInfoList.GetConverter(Me.ConvCode, _orderPrd, String.Empty)
        '            If conv IsNot Nothing Then
        '                _convRate.Float = conv.DefaultRate
        '            End If
        '        End If

        '    Case Else

        'End Select

        pbs.BO.Rules.CalculationRules.Calculator(sender, e)
    End Sub
#End Region

#Region " Business Properties and Methods "
    Private _DTB As String = String.Empty


    Private _lineNo As Integer
    <System.ComponentModel.DataObjectField(True, True)> _
    Public ReadOnly Property LineNo() As String
        Get
            Return _lineNo
        End Get
    End Property

    Private _clientDocId As pbs.Helper.SmartInt32 = New pbs.Helper.SmartInt32(0)
    Public Property ClientDocId() As String
        Get
            Return _clientDocId.Text
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ClientDocId", True)
            If value Is Nothing Then value = String.Empty
            If Not _clientDocId.Equals(value) Then
                _clientDocId.Text = value
                PropertyHasChanged("ClientDocId")
            End If
        End Set
    End Property

    Private _processingDate As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public Property ProcessingDate() As String
        Get
            Return _processingDate.Text
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ProcessingDate", True)
            If value Is Nothing Then value = String.Empty
            If Not _processingDate.Equals(value) Then
                _processingDate.Text = value
                PropertyHasChanged("ProcessingDate")
            End If
        End Set
    End Property

    Private _processingOperation As String = String.Empty
    Public Property ProcessingOperation() As String
        Get
            Return _processingOperation
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ProcessingOperation", True)
            If value Is Nothing Then value = String.Empty
            If Not _processingOperation.Equals(value) Then
                _processingOperation = value
                PropertyHasChanged("ProcessingOperation")
            End If
        End Set
    End Property

    Private _processingBy As String = String.Empty
    Public Property ProcessingBy() As String
        Get
            Return _processingBy
        End Get
        Set(ByVal value As String)
            CanWriteProperty("ProcessingBy", True)
            If value Is Nothing Then value = String.Empty
            If Not _processingBy.Equals(value) Then
                _processingBy = value
                PropertyHasChanged("ProcessingBy")
            End If
        End Set
    End Property

    Private _status As String = String.Empty
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            CanWriteProperty("Status", True)
            If value Is Nothing Then value = String.Empty
            If Not _status.Equals(value) Then
                _status = value
                PropertyHasChanged("Status")
            End If
        End Set
    End Property

    Private _comment As String = String.Empty
    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            CanWriteProperty("Comment", True)
            If value Is Nothing Then value = String.Empty
            If Not _comment.Equals(value) Then
                _comment = value
                PropertyHasChanged("Comment")
            End If
        End Set
    End Property

    Private _updated As pbs.Helper.SmartDate = New pbs.Helper.SmartDate()
    Public ReadOnly Property Updated() As String
        Get
            Return _updated.Text
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

#End Region 'Business Properties and Methods

#Region "Validation Rules"

    Private Sub AddSharedCommonRules()
        'Sample simple custom rule
        'ValidationRules.AddRule(AddressOf LDInfo.ContainsValidPeriod, "Period", 1)           

        'Sample dependent property. when check one , check the other as well
        'ValidationRules.AddDependantProperty("AccntCode", "AnalT0")
    End Sub

    Protected Overrides Sub AddBusinessRules()
        AddSharedCommonRules()

        For Each _field As ClassField In ClassSchema(Of DPRO)._fieldList
            If _field.Required Then
                ValidationRules.AddRule(AddressOf Csla.Validation.CommonRules.StringRequired, _field.FieldName, 0)
            End If
            If Not String.IsNullOrEmpty(_field.RegexPattern) Then
                ValidationRules.AddRule(AddressOf Csla.Validation.CommonRules.RegExMatch, New RegExRuleArgs(_field.FieldName, _field.RegexPattern), 1)
            End If
            '----------using lookup, if no user lookup defined, fallback to predefined by developer----------------------------
            If CATMAPInfoList.ContainsCode(_field) Then
                ValidationRules.AddRule(AddressOf LKUInfoList.ContainsLiveCode, _field.FieldName, 2)
                'Else
                '    Select Case _field.FieldName
                '        Case "LocType"
                '            ValidationRules.AddRule(Of LOC, AnalRuleArg)(AddressOf LOOKUPInfoList.ContainsSysCode, New AnalRuleArg(_field.FieldName, SysCats.LocationType))
                '        Case "Status"
                '            ValidationRules.AddRule(Of LOC, AnalRuleArg)(AddressOf LOOKUPInfoList.ContainsSysCode, New AnalRuleArg(_field.FieldName, SysCats.LocationStatus))
                '    End Select
            End If
        Next
        Rules.BusinessRules.RegisterBusinessRules(Me)
        MyBase.AddBusinessRules()
    End Sub
#End Region ' Validation

#Region " Factory Methods "

    Private Sub New()
        _DTB = Context.CurrentBECode
    End Sub

    Public Shared Function BlankDPRO() As DPRO
        Return New DPRO
    End Function

    Public Shared Function NewDPRO(ByVal pLineNo As String) As DPRO
        'If KeyDuplicated(pLineNo) Then ExceptionThower.BusinessRuleStop(String.Format(ResStr(ResStrConst.NOACCESS), ResStr("DPRO")))
        Return DataPortal.Create(Of DPRO)(New Criteria(pLineNo.ToInteger))
    End Function

    Public Shared Function NewBO(ByVal ID As String) As DPRO
        Dim pLineNo As String = ID.Trim

        Return NewDPRO(pLineNo)
    End Function

    Public Shared Function GetDPRO(ByVal pLineNo As String) As DPRO
        Return DataPortal.Fetch(Of DPRO)(New Criteria(pLineNo.ToInteger))
    End Function

    Public Shared Function GetBO(ByVal ID As String) As DPRO
        Dim pLineNo As String = ID.Trim

        Return GetDPRO(pLineNo)
    End Function

    Public Shared Sub DeleteDPRO(ByVal pLineNo As String)
        DataPortal.Delete(New Criteria(pLineNo.ToInteger))
    End Sub

    Public Overrides Function Save() As DPRO
        If Not IsDirty Then ExceptionThower.NotDirty(ResStr(ResStrConst.NOTDIRTY))
        If Not IsSavable Then Throw New Csla.Validation.ValidationException(String.Format(ResStr(ResStrConst.INVALID), ResStr("DPRO")))

        Me.ApplyEdit()
        DPROInfoList.InvalidateCache()
        Return MyBase.Save()
    End Function

    Public Function CloneDPRO(ByVal pLineNo As String) As DPRO

        'If DPRO.KeyDuplicated(pLineNo) Then ExceptionThower.BusinessRuleStop(ResStr(ResStrConst.CreateAlreadyExists), Me.GetType.ToString.Leaf.Translate)

        Dim cloningDPRO As DPRO = MyBase.Clone
        cloningDPRO._lineNo = 0
        cloningDPRO._DTB = Context.CurrentBECode

        'Todo:Remember to reset status of the new object here 
        cloningDPRO.MarkNew()
        cloningDPRO.ApplyEdit()

        cloningDPRO.ValidationRules.CheckRules()

        Return cloningDPRO
    End Function

#End Region ' Factory Methods

#Region " Data Access "

    <Serializable()> _
    Private Class Criteria
        Public _lineNo As Integer

        Public Sub New(ByVal pLineNo As String)
            _lineNo = pLineNo.ToInteger

        End Sub
    End Class

    <RunLocal()> _
    Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
        _lineNo = criteria._lineNo

        ValidationRules.CheckRules()
    End Sub

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
        Using ctx = ConnectionManager.GetManager
            Using cm = ctx.Connection.CreateCommand()
                cm.CommandType = CommandType.Text
                cm.CommandText = <SqlText>SELECT * FROM pbs_DOC_PROCESS WHERE LINE_NO= <%= criteria._lineNo %></SqlText>.Value.Trim

                Using dr As New SafeDataReader(cm.ExecuteReader)
                    If dr.Read Then
                        FetchObject(dr)
                        MarkOld()
                    End If
                End Using

            End Using
        End Using
    End Sub

    Private Sub FetchObject(ByVal dr As SafeDataReader)
        _lineNo = dr.GetInt32("LINE_NO")
        _clientDocId.Text = dr.GetInt32("CLIENT_DOC_ID")
        _processingDate.Text = dr.GetDateTime("PROCESSING_DATE")
        _processingOperation = dr.GetString("PROCESSING_OPERATION").TrimEnd
        _processingBy = dr.GetString("PROCESSING_BY").TrimEnd
        _status = dr.GetString("STATUS").TrimEnd
        _comment = dr.GetString("COMMENT").TrimEnd
        _updated.Text = dr.GetDateTime("UPDATED")

    End Sub

    Private Shared _lockObj As New Object
    Protected Overrides Sub DataPortal_Insert()
        SyncLock _lockObj
            Using ctx = ConnectionManager.GetManager
                Using cm = ctx.Connection.CreateCommand()

                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "pbs_DOC_PROCESS_Insert"

                    cm.Parameters.AddWithValue("@LINE_NO", _lineNo).Direction = ParameterDirection.Output
                    AddInsertParameters(cm)
                    cm.ExecuteNonQuery()

                End Using
            End Using
        End SyncLock
    End Sub

    Private Sub AddInsertParameters(ByVal cm As SqlCommand)

        cm.Parameters.AddWithValue("@CLIENT_DOC_ID", _clientDocId.DBValue)
        cm.Parameters.AddWithValue("@PROCESSING_DATE", _processingDate.DBValue)
        cm.Parameters.AddWithValue("@PROCESSING_OPERATION", _processingOperation.Trim)
        cm.Parameters.AddWithValue("@PROCESSING_BY", _processingBy.Trim)
        cm.Parameters.AddWithValue("@STATUS", _status.Trim)
        cm.Parameters.AddWithValue("@COMMENT", _comment.Trim)
        cm.Parameters.AddWithValue("@UPDATED", _updated.DBValue)
    End Sub


    Protected Overrides Sub DataPortal_Update()
        SyncLock _lockObj
            Using ctx = ConnectionManager.GetManager
                Using cm = ctx.Connection.CreateCommand()

                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "pbs_DOC_PROCESS_Update"

                    cm.Parameters.AddWithValue("@LINE_NO", _lineNo)
                    AddInsertParameters(cm)
                    cm.ExecuteNonQuery()

                End Using
            End Using
        End SyncLock
    End Sub

    Protected Overrides Sub DataPortal_DeleteSelf()
        DataPortal_Delete(New Criteria(_lineNo))
    End Sub

    Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        Using ctx = ConnectionManager.GetManager
            Using cm = ctx.Connection.CreateCommand()

                cm.CommandType = CommandType.Text
                cm.CommandText = <SqlText>DELETE pbs_DOC_PROCESS WHERE LINE_NO= <%= criteria._lineNo %></SqlText>.Value.Trim
                cm.ExecuteNonQuery()

            End Using
        End Using

    End Sub

    'Protected Overrides Sub DataPortal_OnDataPortalInvokeComplete(ByVal e As Csla.DataPortalEventArgs)
    '    If Csla.ApplicationContext.ExecutionLocation = ExecutionLocations.Server Then
    '        DPROInfoList.InvalidateCache()
    '    End If
    'End Sub


#End Region 'Data Access                           

#Region " Exists "
    Public Shared Function Exists(ByVal pLineNo As String) As Boolean
        Return DPROInfoList.ContainsCode(pLineNo)
    End Function

    '    Public Shared Function KeyDuplicated(ByVal pLineNo As SmartInt32) As Boolean
    '        Dim SqlText = <SqlText>SELECT COUNT(*) FROM pbs_DOC_PROCESS WHERE DTB='<%= Context.CurrentBECode %>'  AND LINE_NO= '<%= pLineNo %>'</SqlText>.Value.Trim
    '        Return SQLCommander.GetScalarInteger(SqlText) > 0
    '    End Function
#End Region

#Region " IGenpart "

    Public Function CloneBO(ByVal id As String) As Object Implements Interfaces.IGenPartObject.CloneBO
        Return CloneDPRO(id)
    End Function

    Public Function getBO1(ByVal id As String) As Object Implements Interfaces.IGenPartObject.GetBO
        Return GetBO(id)
    End Function

    Public Function myCommands() As String() Implements Interfaces.IGenPartObject.myCommands
        Return pbs.Helper.Action.StandardReferenceCommands
    End Function

    Public Function myFullName() As String Implements Interfaces.IGenPartObject.myFullName
        Return GetType(DPRO).ToString
    End Function

    Public Function myName() As String Implements Interfaces.IGenPartObject.myName
        Return GetType(DPRO).ToString.Leaf
    End Function

    Public Function myQueryList() As IList Implements Interfaces.IGenPartObject.myQueryList
        Return DPROInfoList.GetDPROInfoList
    End Function
#End Region

#Region "IDoclink"
    Public Function Get_DOL_Reference() As String Implements IDocLink.Get_DOL_Reference
        Return String.Format("{0}#{1}", Get_TransType, _lineNo)
    End Function

    Public Function Get_TransType() As String Implements IDocLink.Get_TransType
        Return Me.GetType.ToClassSchemaName.Leaf
    End Function
#End Region

End Class

