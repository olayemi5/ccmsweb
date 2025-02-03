Imports System.Data.SqlClient
Imports System.Xml
Imports CCMS.BusinessLogic
Imports CCMS.BusinessObject
Imports CCMS.DataAccess

Public Class Cbnxmlupload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Try
            Dim tracknum As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("tracknum"), Label).Text
            Dim branch_name As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("branch_name"), TextBox).Text
            Dim technician As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("technician"), TextBox).Text
            Dim client_type As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("client_type"), TextBox).Text
            Dim comp_name As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_name"), TextBox).Text
            Dim First_name_pet As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("First_name_pet"), TextBox).Text

            Dim Middle_name_pet As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Middle_name_pet"), TextBox).Text
            Dim Last_name_pet As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Last_name_pet"), TextBox).Text

            Dim acct_num As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_num"), TextBox).Text
            Dim acct_type As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_type"), TextBox).Text

            Dim acct_ccy As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_ccy"), TextBox).Text
            Dim addr_1_comp As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("addr_1_comp"), TextBox).Text

            Dim addr_2_comp As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("addr_2_comp"), TextBox).Text
            Dim acct_owner_city As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_owner_city"), TextBox).Text

            Dim acct_owner_state As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_owner_state"), TextBox).Text
            Dim acct_owner_country As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_owner_country"), TextBox).Text

            Dim acct_owner_pcode As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_owner_pcode"), TextBox).Text
            Dim acct_owner_phnum As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_owner_phnum"), TextBox).Text

            Dim acct_owner_offphnum As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("acct_owner_offphnum"), TextBox).Text
            Dim comp_channel As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_channel"), TextBox).Text

            Dim comp_location As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_location"), TextBox).Text
            Dim comp_email As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_email"), TextBox).Text

            Dim comp_fininmpl As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_fininmpl"), TextBox).Text
            Dim comp_cat As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_cat"), TextBox).Text

            Dim comp_subcat As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_subcat"), TextBox).Text
            Dim comp_subj As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_subj"), TextBox).Text

            Dim comp_desc As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_desc"), TextBox).Text
            'Dim comp_prayer As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_prayer"), TextBox).Text

            Dim comp_date_recv As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_date_recv"), TextBox).Text
            Dim comp_date_clsed As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("comp_date_clsed"), TextBox).Text

            Dim amt_applicable As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("amt_applicable"), TextBox).Text
            Dim amt_refund As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("amt_refund"), TextBox).Text

            Dim Preferred_contact_phone As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Preferred_contact_phone"), TextBox).Text
            Dim action_taken As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("action_taken"), TextBox).Text

            Dim Status As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Status"), TextBox).Text
            Dim bank_remark As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("bank_remark"), TextBox).Text

            Dim Root_cause As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Root_cause"), TextBox).Text
            Dim Preferred_contact_Email As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Preferred_contact_Email"), TextBox).Text

            Dim Preferred_contact_address As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("Preferred_contact_address"), TextBox).Text

            Dim UniqueIdentificationNumber As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("UniqueIdentificationNumber"), TextBox).Text
            Dim amt_recovd As String


            'Get current data
            Dim SqlConnect As New SqlConnection(AppConfig.CCMSDB)
            Dim cmd As New SqlCommand()
            Dim SqlCMBText = "select * FROM [dbo].[ccms_comp_detail2] where  [tracknum] = '" & tracknum & "'"

            cmd = SqlConnect.CreateCommand
            'cmd.CommandType = CommandType.Text
            cmd.CommandText = SqlCMBText
            'cmd.Parameters.Add("@tracknum", SqlDbType.VarChar).Value = tracknum
            SqlConnect.Open()

            Dim myDataReader As SqlDataReader = cmd.ExecuteReader
            'Dim reader As SqlDataReader = cmd.ExecuteReader
            myDataReader.Read()
            If (myDataReader.HasRows) Then
                'While ()
                amt_recovd = Convert.ToString(myDataReader("amt_recovd"))
                'End While
            Else
                ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + "Invalid record selected" + "');", True)
            End If
            SqlConnect.Close()

            Dim ocomplaindetail As New complaindtl
            ocomplaindetail.branch_name = branch_name
            ocomplaindetail.technician = technician
            ocomplaindetail.client_type = client_type
            ocomplaindetail.comp_name = comp_name
            ocomplaindetail.First_name_pet = First_name_pet
            ocomplaindetail.Middle_name_pet = Middle_name_pet
            ocomplaindetail.Last_name_pet = Last_name_pet
            ocomplaindetail.acct_num = acct_num
            ocomplaindetail.acct_type = acct_type
            ocomplaindetail.acct_ccy = acct_ccy

            ocomplaindetail.addr_1_comp = addr_1_comp
            ocomplaindetail.addr_2_comp = addr_2_comp
            ocomplaindetail.acct_owner_city = acct_owner_city
            ocomplaindetail.acct_owner_state = acct_owner_state
            ocomplaindetail.acct_owner_country = acct_owner_country
            ocomplaindetail.acct_owner_pcode = acct_owner_pcode
            ocomplaindetail.acct_owner_phnum = acct_owner_phnum
            ocomplaindetail.acct_owner_offphnum = acct_owner_offphnum
            ocomplaindetail.comp_channel = comp_channel
            ocomplaindetail.comp_location = comp_location

            ocomplaindetail.comp_email = comp_email
            ocomplaindetail.comp_fininmpl = comp_fininmpl
            ocomplaindetail.comp_cat = comp_cat
            ocomplaindetail.comp_subcat = comp_subcat
            ocomplaindetail.comp_subj = comp_subj
            ocomplaindetail.comp_desc = comp_desc
            ocomplaindetail.comp_prayer = "Customer wants their issues resolved"
            ocomplaindetail.comp_date_recv = Convert.ToDateTime(comp_date_recv)
            ocomplaindetail.comp_date_clsed = Convert.ToDateTime(comp_date_clsed)
            ocomplaindetail.amt_applicable = CDbl(amt_applicable)

            ocomplaindetail.amt_refund = CDbl(amt_refund)
            ocomplaindetail.amt_recovd = CDbl(amt_recovd)
            ocomplaindetail.action_taken = action_taken
            ocomplaindetail.status = Status
            ocomplaindetail.tracknum = tracknum
            ocomplaindetail.bank_remark = bank_remark
            ocomplaindetail.UniqueIdentificationNumber = UniqueIdentificationNumber
            Dim result As Integer = Complainmanager2.editCompDetail(ocomplaindetail)

            If result > 0 Then
                ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + "Records sucessfully updated" + "');", True)

                GridView1.EditIndex = -1
                GridView1.DataSource = Ccmsdetail2.getCCMSdetailalll()
                GridView1.DataBind()

            Else
                ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + "Record cannot be updated" + "');", True)

            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(GetType(String), "myalert", "alert('" + ex.Message.ToLower + "');", True)
        End Try

    End Sub


  
    Protected Sub GenerateXML_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim ComplainDetails As New Complaindetaillist2
        ComplainDetails = Ccmsdetail2.getCCMSdetailalll()
        GridView1.DataSource = ComplainDetails
        GridView1.DataBind()

        Dim settings As XmlWriterSettings = New XmlWriterSettings()

        Dim CallerReportBody As New BODY
        Dim _callreport As New CALL_REPORT
        Dim header As New HEADER
        header.AS_AT = DateTime.Now.ToString("dd-MM-yyyy")
        header.CALLREPORT_DESC = "Submissions on complaints from the reporting institutions"
        'header.CALLREPORT_ID = "CCMSFCMB" & DateTime.Now.ToString("ddMMyyyymmss")
        header.CALLREPORT_ID = "CCMS100"
        header.INST_CODE = "00214"
        header.INST_NAME = "FIRST CITY MONUMENT BANK LIMITED"
        Dim CALL_REPORT As New CALLREPORT



        Dim FOOTER As New FOOTER
        Dim AUTH_SIGNATORY As New AUTH_SIGNATORY

        AUTH_SIGNATORY.DESIGNATION = "SYSTEM"
        AUTH_SIGNATORY.EXTN = "NA"
        AUTH_SIGNATORY.NAME = "NA"
        AUTH_SIGNATORY.POSITION = "NA"
        AUTH_SIGNATORY.TEL_NO = "NA"
        AUTH_SIGNATORY.__DATE = DateTime.Now.ToString("dd-MM-yyyy")

        FOOTER.AUTH_BY = "SYSTEM"
        FOOTER.AUTH_SIGNATORY = AUTH_SIGNATORY
        FOOTER.HEAD_OFFICE_ADDRESS = "17A Tinubu Street,Lagos Island"
        FOOTER.PREPARED_BY = "SYSTEM"
        FOOTER.PREPARED_DATE = DateTime.Now.ToString("dd-MM-yyyy")
        FOOTER.TEL_NO = "NA"
        FOOTER.__DATE = DateTime.Now.ToString("dd-MM-yyyy")
        FOOTER.DESC = "NA"
        FOOTER.CHECKED_BY = "NA"
        FOOTER.CHECKED_DATE = DateTime.Now.ToString("dd-MM-yyyy")


        Dim CONTACT_DETAILS As New CONTACT_DETAILS
        CONTACT_DETAILS.DESIGNATION = "NA"
        CONTACT_DETAILS.EXTN = "NA"
        CONTACT_DETAILS.NAME = "NA"
        CONTACT_DETAILS.TEL_NO = "NA"
        CONTACT_DETAILS.__DATE = DateTime.Now.ToString("dd-MM-yyyy")


        FOOTER.CONTACT_DETAILS = CONTACT_DETAILS

        Dim _DATALIST As New List(Of CALLREPORT_DATA)
        For Each o In ComplainDetails
            Dim _DATA As New CALLREPORT_DATA


            _DATA.ACCOUNT_CURRENCY = o.acct_ccy
            _DATA.ACCOUNT_NUMBER_NUBAN = o.acct_num.PadLeft(10, "0")
            _DATA.ACCOUNT_TYPE = o.acct_type
            _DATA.ACTION_TAKEN = o.action_taken
            _DATA.AMOUNT_IN_DISPUTE = o.amt_applicable
            _DATA.AMOUNT_RECOVERED_BY_BANK = o.amt_recovd
            _DATA.AMOUNT_REFUNDED_TO_PETITIONER = o.amt_refund
            _DATA.BRANCH_NAME = o.branch_name
            _DATA.CELL_PHONE_NUMBER = covertMobileTo(o.acct_owner_phnum)
            _DATA.CITY = o.acct_owner_city
            _DATA.COMPLAINT_CATEGORY_CODE = o.comp_subcat
            _DATA.COMPLAINT_DESCRIPTION = o.comp_desc
            _DATA.COMPLAINT_LOCATION_BRANCH = o.branch_name
            _DATA.COMPLAINT_LOCATION_CITY = o.acct_owner_city
            _DATA.COMPLIANT_CHANNEL = o.comp_channel
            '_DATA.COUNTRY = o.acct_owner_country
            _DATA.DATE_CLOSED = o.comp_date_clsed.ToString("dd-MM-yyyy")
            _DATA.DATE_RECEIVED = o.comp_date_recv.ToString("dd-MM-yyyy")
            _DATA.EMAIL_ADDRESS = o.comp_email
            _DATA.FIRSTNAME = o.First_name_pet
            _DATA.LASTNAME = o.Last_name_pet
            _DATA.MIDDLENAME = o.Middle_name_pet
            _DATA.NAME_OF_COMPLAINANT_PETITIONER = o.comp_name
            _DATA.NAME_OF_CONSULTANT = o.technician
            _DATA.OFFICE_NUMBER = covertMobileTo(o.acct_owner_offphnum)
            _DATA.POSTAL_CODE = o.acct_owner_pcode
            _DATA.REMARKS_BY_THE_BANK = o.bank_remark
            _DATA.STATE = o.acct_owner_state
            _DATA.STATUS_OF_THE_COMPLAINT = o.status
            _DATA.STREET_ADDRESS = o.addr_1_comp
            _DATA.STREET_ADDRESS_2 = o.addr_2_comp
            _DATA.SUBJECT_OF_COMPLAINT = o.comp_subj
            _DATA.TRACKING_REFERENCE_NUMBER = o.tracknum
            _DATA.TYPE_OF_CLIENT = o.client_type
            _DATA.UNIQUE_IDENTIFICATION_NO = o.UniqueIdentificationNumber

            _DATA.COUNTRY = CountryCodesDA.getCountryCodes(o.acct_owner_country).Alpha_2_Code

            If _DATA.EMAIL_ADDRESS.ToUpper = "N/A" Then
                _DATA.EMAIL_ADDRESS = "noemail@fcmb.com"
            End If

            If String.IsNullOrEmpty(o.action_taken) Or String.IsNullOrWhiteSpace(o.action_taken) Then
                _DATA.ACTION_TAKEN = "NA"
            End If
            If String.IsNullOrEmpty(o.UniqueIdentificationNumber) Or String.IsNullOrWhiteSpace(o.UniqueIdentificationNumber) Then
                _DATA.UNIQUE_IDENTIFICATION_NO = "NA"
            End If
            If String.IsNullOrEmpty(_DATA.REMARKS_BY_THE_BANK) Or String.IsNullOrWhiteSpace(_DATA.REMARKS_BY_THE_BANK) Then
                _DATA.REMARKS_BY_THE_BANK = "NA"
            End If

            If _DATA.STATUS_OF_THE_COMPLAINT <> "NEW" Or _DATA.STATUS_OF_THE_COMPLAINT <> "ASSIGNED" Or _DATA.STATUS_OF_THE_COMPLAINT <> "RESOLVED" Or _DATA.STATUS_OF_THE_COMPLAINT <> "CLOSED" Or _DATA.STATUS_OF_THE_COMPLAINT <> "REJECTED" Then
                _DATA.STATUS_OF_THE_COMPLAINT = "PENDING_INPUT"
            End If
            If _DATA.COMPLIANT_CHANNEL <> "EMAIL" Or _DATA.COMPLIANT_CHANNEL <> "MOBILE_APP" Or _DATA.COMPLIANT_CHANNEL <> "SOCIAL_MEDIA" Or _DATA.COMPLIANT_CHANNEL <> "LETTER" Or _DATA.COMPLIANT_CHANNEL <> "WALK_IN" Or _DATA.COMPLIANT_CHANNEL <> "PHONE" Or _DATA.COMPLIANT_CHANNEL <> "CHAT" Or _DATA.COMPLIANT_CHANNEL <> "WEBSITE" Then
                _DATA.COMPLIANT_CHANNEL = "OTHERS"
            End If

            CallerReportBody.Add(_DATA)



        Next
        '  CallerReportBody = _DATALIST.ToList()


        CALL_REPORT.BODY = CallerReportBody
        CALL_REPORT.FOOTER = FOOTER
        CALL_REPORT.HEADER = header

        _callreport.CALLREPORT = CALL_REPORT


        settings.Indent = True
        Dim filename As String = DateTime.Now().ToString("ddMMyyyymmss") & "_ComplainDetails.xml"
        '  Dim filename As String = "CCMS100.xml"
        Dim filepath As String = AppDomain.CurrentDomain.BaseDirectory() & "xmlfiles\" & filename
        Dim x As New System.Xml.Serialization.XmlSerializer(_callreport.CALLREPORT.GetType)
        Dim file As New System.IO.StreamWriter(filepath)
        x.Serialize(file, _callreport.CALLREPORT)
        file.Close()
        DownloadXML.Visible = True

        XMLPath.Value = filepath
        XMLFilename.Value = filename

    End Sub

    Protected Sub DownloadXML_Click(sender As Object, e As EventArgs) Handles DownloadXML.Click
        Response.ContentType = "application/xml"
        Response.AppendHeader("Content-Disposition", "attachment; filename=CCMS100.xml")

        Response.ContentType = "Application/xml"
        'Get the physical path to the file.
        Dim FilePath = XMLPath.Value
        'Write the file directly to the HTTP content output stream.
        Response.TransmitFile(FilePath)
        Response.End()
    End Sub

    Private Function covertMobileTo(number As String) As String
        Dim value As String
        If number.Length >= 11 And number.StartsWith("234") = False Then
            value = "(234) " & number.Substring(1, 3) & "-" & number.Substring(number.Length - 7)
        ElseIf number.Length = 10 Or (number.StartsWith("234") And number.Length = 10) Then
            value = "(234) " & number.Substring(0, 3) & "-" & number.Substring(number.Length - 7)
        Else
            number = number.PadLeft(11, "0")
            value = "(234) " & number.Substring(1, 3) & "-" & number.Substring(number.Length - 7)
        End If

        Return value


    End Function


End Class