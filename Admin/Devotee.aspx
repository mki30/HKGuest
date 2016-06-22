<%@ Page Title="Devotee" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Devotee.aspx.cs" Inherits="Admin_Devotee" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        var error = "Not logged in";
        $(function ()
        {
            $("#txtDOB").datepicker({
                changeMonth: true,
                changeYear: true,
            });
            $("#txtDOJ").datepicker();
            $("#txtSpouseDOB").datepicker({
                changeMonth: true,
                changeYear: true,
            });
            $("#txtAnniversary").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                });
            $("#txtChild_DoB").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                });
        });
    </script>
    <style>
        .req
        {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top; width: 20%">
                <asp:Literal ID="ltPerson" runat="server"></asp:Literal>
            </td>
            <td style="vertical-align: top; width: 80%">
                <%--<h3>Register</h3>--%>
                <asp:Label ID="lblID" runat="server" Style="display: none; width: 100%" />
                <table id="Table1" runat="server" style="background-color: #e5e5e5;" class="table-padded">
                    <tr>
                        <td>Name<span class="req">*</span><br />
                            <asp:TextBox MaxLength="45" ID="txtDevName" runat="server" value="" required placeholder="NAME" /></td>
                        <td>Contact<span class="req">*</span><br />
                            <asp:TextBox MaxLength="10" ID="txtContact" runat="server" required placeholder="CONTACT" /></td>
                        <td>Alternate Contact<br />
                            <asp:TextBox MaxLength="10" ID="txtAltContact" runat="server" placeholder="ALT CONTACT" /></td>
                        <td>Birthday Self<span class="req">*</span><br />
                            <asp:TextBox MaxLength="10" ID="txtDOB" runat="server" required placeholder="BIRTHDAY" /></td>
                    </tr>
                    <tr>
                        <td>Date Of Joining<br />
                            <asp:TextBox MaxLength="10" ID="txtDOJ" runat="server" placeholder="DATE OF JOINING" /></td>
                        <td>Address<br />
                            <asp:TextBox MaxLength="255" ID="txtAddress" runat="server" placeholder="ADDRESS" /></td>
                        <td>Mail<br />
                            <asp:TextBox MaxLength="128" ID="txteMail" runat="server" placeholder="EMAIL" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Spouse Name<br />
                            <asp:TextBox MaxLength="45" ID="txtSpouse" runat="server" placeholder="SPOUSE NAME" /></td>
                        <td>Birthday Spouse
                        <br />
                            <asp:TextBox MaxLength="10" ID="txtSpouseDOB" runat="server" placeholder="SPOUSE DOB" /></td>
                        <td>Anniversary<br />
                            <asp:TextBox MaxLength="10" ID="txtAnniversary" runat="server" placeholder="ANNIVERSARY" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Age<br />
                            <asp:TextBox MaxLength="45" ID="txtAge" runat="server" placeholder="AGE" /></td>
                        <td>AgeGroup<br />
                            <asp:DropDownList ID="ddAgeGroup" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="0 - 10">0 - 10</asp:ListItem>
                                <asp:ListItem Value="11 - 15">11 - 15</asp:ListItem>
                                <asp:ListItem Value="16 - 20">16 - 20</asp:ListItem>
                                <asp:ListItem Value="21 - 25">21 - 25</asp:ListItem>
                                <asp:ListItem Value="26 - 30">26 - 30</asp:ListItem>
                                <asp:ListItem Value="31 - 35">31 - 35</asp:ListItem>
                                <asp:ListItem Value="35 - 40">35 - 40</asp:ListItem>
                                <asp:ListItem Value="40 - 45">40 - 45</asp:ListItem>
                                <asp:ListItem Value="45 - 50">45 - 50</asp:ListItem>
                                <asp:ListItem Value="50 - 60">50 - 60</asp:ListItem>
                                <asp:ListItem Value="60 - 75">60 - 75</asp:ListItem>
                                <asp:ListItem Value="75+">75+</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Gender<br />
                            <asp:DropDownList ID="ddGender" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Not Specified">Not Specified</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>Education<br />
                            <asp:DropDownList ID="ddEducation" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Advocate">Advocate</asp:ListItem>
                                <asp:ListItem Value="CA / CS">CA / CS</asp:ListItem>
                                <asp:ListItem Value="Creative Diploma">Creative Diploma</asp:ListItem>
                                <asp:ListItem Value="Doctor">Doctor</asp:ListItem>
                                <asp:ListItem Value="Engineer">Engineer</asp:ListItem>
                                <asp:ListItem Value="Engineer - Civil">Engineer - Civil</asp:ListItem>
                                <asp:ListItem Value="Engineer - IT">Engineer - IT</asp:ListItem>
                                <asp:ListItem Value="Graduate">Graduate</asp:ListItem>
                                <asp:ListItem Value="Graduate - Art">Graduate - Art</asp:ListItem>
                                <asp:ListItem Value="Graduate - Com">Graduate - Com</asp:ListItem>
                                <asp:ListItem Value="Graduate - Creative">Graduate - Creative</asp:ListItem>
                                <asp:ListItem Value="Graduate - Sc">Graduate - Sc</asp:ListItem>
                                <asp:ListItem Value="MBA">MBA</asp:ListItem>
                                <asp:ListItem Value="MCA">MCA</asp:ListItem>
                                <asp:ListItem Value="Medico">Medico</asp:ListItem>
                                <asp:ListItem Value="Not Available">Not Available</asp:ListItem>
                                <asp:ListItem Value="Not Specified">Not Specified</asp:ListItem>
                                <asp:ListItem Value="Post Grad">Post Grad</asp:ListItem>
                                <asp:ListItem Value="Post Grad - Art">Post Grad - Art</asp:ListItem>
                                <asp:ListItem Value="Post Grad - Com">Post Grad - Com</asp:ListItem>
                                <asp:ListItem Value="Post Grad - Sc">Post Grad - Sc</asp:ListItem>
                                <asp:ListItem Value="Schooling">Schooling</asp:ListItem>
                                <asp:ListItem Value="Under Grad">Under Grad</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Profession<br />
                            <asp:DropDownList ID="ddProfession" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Accounts">Accounts</asp:ListItem>
                                <asp:ListItem Value="Advocate">Advocate</asp:ListItem>
                                <asp:ListItem Value="Banking">Banking</asp:ListItem>
                                <asp:ListItem Value="Business">Business</asp:ListItem>
                                <asp:ListItem Value="CA">CA</asp:ListItem>
                                <asp:ListItem Value="Creative">Creative</asp:ListItem>
                                <asp:ListItem Value="defence">defence</asp:ListItem>
                                <asp:ListItem Value="Education">Education</asp:ListItem>
                                <asp:ListItem Value="Engineering">Engineering</asp:ListItem>
                                <asp:ListItem Value="Govt Employee">Govt Employee</asp:ListItem>
                                <asp:ListItem Value="Hospitality">Hospitality</asp:ListItem>
                                <asp:ListItem Value="House Wife">House Wife</asp:ListItem>
                                <asp:ListItem Value="Jobless">Jobless</asp:ListItem>
                                <asp:ListItem Value="Management">Management</asp:ListItem>
                                <asp:ListItem Value="Medico">Medico</asp:ListItem>
                                <asp:ListItem Value="Not Specified">Not Specified</asp:ListItem>
                                <asp:ListItem Value="Private Job">Private Job</asp:ListItem>
                                <asp:ListItem Value="Retired">Retired</asp:ListItem>
                                <asp:ListItem Value="Self Employed">Self Employed</asp:ListItem>
                                <asp:ListItem Value="Service">Service</asp:ListItem>
                                <asp:ListItem Value="Student">Student</asp:ListItem>
                                <asp:ListItem Value="Technician">Technician</asp:ListItem>
                                <asp:ListItem Value="Travel">Travel</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Specific Work<br />
                            <asp:TextBox MaxLength="45" ID="txtSpecific_Work" runat="server" placeholder="SPECIFIC WORK" /></td>
                        <td>Nature of Work<br />
                            <asp:DropDownList ID="ddNature_of_Work" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Accounts">Accounts</asp:ListItem>
                                <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                <asp:ListItem Value="Banking">Banking</asp:ListItem>
                                <asp:ListItem Value="Beautician">Beautician</asp:ListItem>
                                <asp:ListItem Value="BPO">BPO</asp:ListItem>
                                <asp:ListItem Value="Business">Business</asp:ListItem>
                                <asp:ListItem Value="CA/CS">CA/CS</asp:ListItem>
                                <asp:ListItem Value="Catering">Catering</asp:ListItem>
                                <asp:ListItem Value="Clarical">Clarical</asp:ListItem>
                                <asp:ListItem Value="Coaching">Coaching</asp:ListItem>
                                <asp:ListItem Value="Construction">Construction</asp:ListItem>
                                <asp:ListItem Value="Creative">Creative</asp:ListItem>
                                <asp:ListItem Value="Doctor">Doctor</asp:ListItem>
                                <asp:ListItem Value="Engineering">Engineering</asp:ListItem>
                                <asp:ListItem Value="Furniture">Furniture</asp:ListItem>
                                <asp:ListItem Value="Garments">Garments</asp:ListItem>
                                <asp:ListItem Value="Govt">Govt</asp:ListItem>
                                <asp:ListItem Value="Interior">Interior</asp:ListItem>
                                <asp:ListItem Value="IT">IT</asp:ListItem>
                                <asp:ListItem Value="Jewellary">Jewellary</asp:ListItem>
                                <asp:ListItem Value="Management">Management</asp:ListItem>
                                <asp:ListItem Value="Media">Media</asp:ListItem>
                                <asp:ListItem Value="Medical">Medical</asp:ListItem>
                                <asp:ListItem Value="Police">Police</asp:ListItem>
                                <asp:ListItem Value="Software">Software</asp:ListItem>
                                <asp:ListItem Value="Teaching">Teaching</asp:ListItem>
                                <asp:ListItem Value="Technician">Technician</asp:ListItem>
                                <asp:ListItem Value="Transport">Transport</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Chanting<br />
                            <asp:DropDownList ID="ddChanting" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="None">None</asp:ListItem>
                                <asp:ListItem Value="Upto 12">Upto 12</asp:ListItem>
                                <asp:ListItem Value="Upto 16">Upto 16</asp:ListItem>
                                <asp:ListItem Value="Upto 4">Upto 4</asp:ListItem>
                                <asp:ListItem Value="Upto 8">Upto 8</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Married<br />
                            <asp:DropDownList ID="ddMarried" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Kids<br />
                            <asp:DropDownList ID="ddKids" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="None">None</asp:ListItem>
                                <asp:ListItem Value="Not Specified">Not Specified</asp:ListItem>
                                <asp:ListItem Value="One">One</asp:ListItem>
                                <asp:ListItem Value="Three">Three</asp:ListItem>
                                <asp:ListItem Value="Three +">Three +</asp:ListItem>
                                <asp:ListItem Value="Two">Two</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Child Name<br />
                            <asp:TextBox runat="server" MaxLength="45" ID="txtChildName" placeholder="CHILD NAME" /></td>
                        <td>Child Age<br />
                            <asp:TextBox MaxLength="45" ID="txtChild_Age" runat="server" placeholder="CHILD AGE" /></td>
                    </tr>
                    <tr>
                        <td>Child DoB<br />
                            <asp:TextBox MaxLength="45" ID="txtChild_DoB" runat="server" placeholder="CHILD DOB" /></td>
                        <td>Language1<br />
                            <asp:DropDownList ID="ddLanguage1" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Bengali">Bengali</asp:ListItem>
                                <asp:ListItem Value="English">English</asp:ListItem>
                                <asp:ListItem Value="Hindi">Hindi</asp:ListItem>
                                <asp:ListItem Value="Maithili">Maithili</asp:ListItem>
                                <asp:ListItem Value="Malyalam">Malyalam</asp:ListItem>
                                <asp:ListItem Value="Manipuri">Manipuri</asp:ListItem>
                                <asp:ListItem Value="Marathi">Marathi</asp:ListItem>
                                <asp:ListItem Value="Oriya">Oriya</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                <asp:ListItem Value="Punjabi">Punjabi</asp:ListItem>
                                <asp:ListItem Value="Rajasthani">Rajasthani</asp:ListItem>
                                <asp:ListItem Value="Sindhi">Sindhi</asp:ListItem>
                                <asp:ListItem Value="Telagu">Telagu</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Language2<br />
                            <asp:DropDownList ID="ddLanguage2" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Bengali">Bengali</asp:ListItem>
                                <asp:ListItem Value="English">English</asp:ListItem>
                                <asp:ListItem Value="Hindi">Hindi</asp:ListItem>
                                <asp:ListItem Value="Maithili">Maithili</asp:ListItem>
                                <asp:ListItem Value="Malyalam">Malyalam</asp:ListItem>
                                <asp:ListItem Value="Manipuri">Manipuri</asp:ListItem>
                                <asp:ListItem Value="Marathi">Marathi</asp:ListItem>
                                <asp:ListItem Value="Oriya">Oriya</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                <asp:ListItem Value="Punjabi">Punjabi</asp:ListItem>
                                <asp:ListItem Value="Rajasthani">Rajasthani</asp:ListItem>
                                <asp:ListItem Value="Sindhi">Sindhi</asp:ListItem>
                                <asp:ListItem Value="Telagu">Telagu</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Language3<br />
                            <asp:DropDownList ID="ddLanguage3" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Bengali">Bengali</asp:ListItem>
                                <asp:ListItem Value="English">English</asp:ListItem>
                                <asp:ListItem Value="Hindi">Hindi</asp:ListItem>
                                <asp:ListItem Value="Maithili">Maithili</asp:ListItem>
                                <asp:ListItem Value="Malyalam">Malyalam</asp:ListItem>
                                <asp:ListItem Value="Manipuri">Manipuri</asp:ListItem>
                                <asp:ListItem Value="Marathi">Marathi</asp:ListItem>
                                <asp:ListItem Value="Oriya">Oriya</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                <asp:ListItem Value="Punjabi">Punjabi</asp:ListItem>
                                <asp:ListItem Value="Rajasthani">Rajasthani</asp:ListItem>
                                <asp:ListItem Value="Sindhi">Sindhi</asp:ListItem>
                                <asp:ListItem Value="Telagu">Telagu</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>ForiegnLang<br />
                            <asp:DropDownList ID="ddForiegnLang" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="French">French</asp:ListItem>
                                <asp:ListItem Value="German">German</asp:ListItem>
                                <asp:ListItem Value="Italian">Italian</asp:ListItem>
                                <asp:ListItem Value="Russian">Russian</asp:ListItem>
                                <asp:ListItem Value="Spanish">Spanish</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Interest<br />
                            <asp:DropDownList ID="ddInterest" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="BG Class">BG Class</asp:ListItem>
                                <asp:ListItem Value="BG Course">BG Course</asp:ListItem>
                                <asp:ListItem Value="Book Distribution">Book Distribution</asp:ListItem>
                                <asp:ListItem Value="Counseling">Counseling</asp:ListItem>
                                <asp:ListItem Value="Cow Protection">Cow Protection</asp:ListItem>
                                <asp:ListItem Value="Deity Services">Deity Services</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Category<br />
                            <asp:DropDownList ID="ddCategory" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="General">General</asp:ListItem>
                                <asp:ListItem Value="VIP">VIP</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Conn to ISKCON<br />
                            <asp:DropDownList ID="ddConn_to_ISKCON" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>ISKCON Months<br />
                            <asp:TextBox MaxLength="45" ID="txtISKCON_Months" runat="server" placeholder="ISKCON MONTHS" /></td>
                        <td>ISKCON Years<br />
                            <asp:TextBox MaxLength="45" ID="txtISKCON_Years" runat="server" placeholder="ISKCON YEARS" /></td>
                        <td>Preaching Group<br />
                            <asp:TextBox MaxLength="45" ID="txtPreaching_Group" runat="server" placeholder="PREACHING GROUP" /></td>
                        <td>Group Leader Name<br />
                            <asp:TextBox MaxLength="45" ID="txtGroup_Leader_Name" runat="server" placeholder="GROUP LEADER NAME" /></td>
                    </tr>
                    <tr>
                        <td>InitiatedName<br />
                            <asp:TextBox MaxLength="45" ID="txtInitiatedName" runat="server" placeholder="INITIATED NAME" /></td>
                        <td>Conn To Other<br />
                            <asp:DropDownList ID="ddConn_To_Other" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Other Spirit. Org.<br />
                            <asp:TextBox MaxLength="45" ID="txtOther_Spirit__Org_" runat="server" value="" placeholder="OTHER SPIRIT. ORG" /></td>
                        <td>Locality<br />
                            <asp:DropDownList ID="ddLocality" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Alakhnanda">Alakhnanda</asp:ListItem>
                                <asp:ListItem Value="Amar Coloney">Amar Coloney</asp:ListItem>
                                <asp:ListItem Value="Anand Parbat">Anand Parbat</asp:ListItem>
                                <asp:ListItem Value="Andrew Ganj">Andrew Ganj</asp:ListItem>
                                <asp:ListItem Value="Asharam">Asharam</asp:ListItem>
                                <asp:ListItem Value="Badarpur">Badarpur</asp:ListItem>
                                <asp:ListItem Value="Bhogal">Bhogal</asp:ListItem>
                                <asp:ListItem Value="C R Park">C R Park</asp:ListItem>
                                <asp:ListItem Value="Chirag Delhi">Chirag Delhi</asp:ListItem>
                                <asp:ListItem Value="Dariya Ganj">Dariya Ganj</asp:ListItem>
                                <asp:ListItem Value="Defence Coloney">Defence Coloney</asp:ListItem>
                                <asp:ListItem Value="Dwarka">Dwarka</asp:ListItem>
                                <asp:ListItem Value="E O K">E O K</asp:ListItem>
                                <asp:ListItem Value="East Delhi">East Delhi</asp:ListItem>
                                <asp:ListItem Value="East of Kailash">East of Kailash</asp:ListItem>
                                <asp:ListItem Value="Faridabad">Faridabad</asp:ListItem>
                                <asp:ListItem Value="G K I">G K I</asp:ListItem>
                                <asp:ListItem Value="Gandhi Nagar">Gandhi Nagar</asp:ListItem>
                                <asp:ListItem Value="Geeta Colony">Geeta Colony</asp:ListItem>
                                <asp:ListItem Value="Ghari">Ghari</asp:ListItem>
                                <asp:ListItem Value="Ghaziabad">Ghaziabad</asp:ListItem>
                                <asp:ListItem Value="Gobind Puri">Gobind Puri</asp:ListItem>
                                <asp:ListItem Value="Gurgaon">Gurgaon</asp:ListItem>
                                <asp:ListItem Value="Harkesh Nr">Harkesh Nr</asp:ListItem>
                                <asp:ListItem Value="Hudson Lane">Hudson Lane</asp:ListItem>
                                <asp:ListItem Value="Indirapuram">Indirapuram</asp:ListItem>
                                <asp:ListItem Value="Janak Puri">Janak Puri</asp:ListItem>
                                <asp:ListItem Value="Jangpura">Jangpura</asp:ListItem>
                                <asp:ListItem Value="Jangpura Extn.">Jangpura Extn.</asp:ListItem>
                                <asp:ListItem Value="Jasola Vihar">Jasola Vihar</asp:ListItem>
                                <asp:ListItem Value="Jbts Garden">Jbts Garden</asp:ListItem>
                                <asp:ListItem Value="Jetpur">Jetpur</asp:ListItem>
                                <asp:ListItem Value="Kailash Colony">Kailash Colony</asp:ListItem>
                                <asp:ListItem Value="Kalka Ji">Kalka Ji</asp:ListItem>
                                <asp:ListItem Value="Karol Bagh">Karol Bagh</asp:ListItem>
                                <asp:ListItem Value="Kaushambi">Kaushambi</asp:ListItem>
                                <asp:ListItem Value="Khan Pur">Khan Pur</asp:ListItem>
                                <asp:ListItem Value="Kidwai Nagar">Kidwai Nagar</asp:ListItem>
                                <asp:ListItem Value="Kotla">Kotla</asp:ListItem>
                                <asp:ListItem Value="Krishi Vihar">Krishi Vihar</asp:ListItem>
                                <asp:ListItem Value="Krishna Nagar">Krishna Nagar</asp:ListItem>
                                <asp:ListItem Value="Lado Sarai">Lado Sarai</asp:ListItem>
                                <asp:ListItem Value="Lajpat Nagar">Lajpat Nagar</asp:ListItem>
                                <asp:ListItem Value="Lodhi Colony">Lodhi Colony</asp:ListItem>
                                <asp:ListItem Value="Luxmi Nagar">Luxmi Nagar</asp:ListItem>
                                <asp:ListItem Value="Madan Gir">Madan Gir</asp:ListItem>
                                <asp:ListItem Value="Madan Puri Khadar">Madan Puri Khadar</asp:ListItem>
                                <asp:ListItem Value="Maharani Bagh">Maharani Bagh</asp:ListItem>
                                <asp:ListItem Value="Mahavir Enclave">Mahavir Enclave</asp:ListItem>
                                <asp:ListItem Value="Mall Road">Mall Road</asp:ListItem>
                                <asp:ListItem Value="Malviya Nagar">Malviya Nagar</asp:ListItem>
                                <asp:ListItem Value="Mansarovar Garden">Mansarovar Garden</asp:ListItem>
                                <asp:ListItem Value="Mount Kailash">Mount Kailash</asp:ListItem>
                                <asp:ListItem Value="Mukherji Ngr">Mukherji Ngr</asp:ListItem>
                                <asp:ListItem Value="Narouji Ngr">Narouji Ngr</asp:ListItem>
                                <asp:ListItem Value="Nehru Nagar">Nehru Nagar</asp:ListItem>
                                <asp:ListItem Value="New Friends Coloney">New Friends Coloney</asp:ListItem>
                                <asp:ListItem Value="Nizamuddin">Nizamuddin</asp:ListItem>
                                <asp:ListItem Value="Okhla">Okhla</asp:ListItem>
                                <asp:ListItem Value="Outstation">Outstation</asp:ListItem>
                                <asp:ListItem Value="Paharganj">Paharganj</asp:ListItem>
                                <asp:ListItem Value="Palam">Palam</asp:ListItem>
                                <asp:ListItem Value="Panchsheel">Panchsheel</asp:ListItem>
                                <asp:ListItem Value="Pandara Road">Pandara Road</asp:ListItem>
                                <asp:ListItem Value="Patparganj">Patparganj</asp:ListItem>
                                <asp:ListItem Value="Prasant Vihar">Prasant Vihar</asp:ListItem>
                                <asp:ListItem Value="R K Puram">R K Puram</asp:ListItem>
                                <asp:ListItem Value="Rohini">Rohini</asp:ListItem>
                                <asp:ListItem Value="Rush Vihar">Rush Vihar</asp:ListItem>
                                <asp:ListItem Value="Sadar Bazar">Sadar Bazar</asp:ListItem>
                                <asp:ListItem Value="Sadiq Nagar">Sadiq Nagar</asp:ListItem>
                                <asp:ListItem Value="Sahibabad">Sahibabad</asp:ListItem>
                                <asp:ListItem Value="Sangam Vihar">Sangam Vihar</asp:ListItem>
                                <asp:ListItem Value="Sant Nagar">Sant Nagar</asp:ListItem>
                                <asp:ListItem Value="Sarita Vihar">Sarita Vihar</asp:ListItem>
                                <asp:ListItem Value="Sarojini Nagar">Sarojini Nagar</asp:ListItem>
                                <asp:ListItem Value="Sarva P Vihar">Sarva P Vihar</asp:ListItem>
                                <asp:ListItem Value="Shakarpur">Shakarpur</asp:ListItem>
                                <asp:ListItem Value="Shalimar Bagh">Shalimar Bagh</asp:ListItem>
                                <asp:ListItem Value="Sheikh Sarai">Sheikh Sarai</asp:ListItem>
                                <asp:ListItem Value="South Delhi">South Delhi</asp:ListItem>
                                <asp:ListItem Value="South Ext.">South Ext.</asp:ListItem>
                                <asp:ListItem Value="Sri Nivas Puri">Sri Nivas Puri</asp:ListItem>
                                <asp:ListItem Value="Sukhdev Bihar">Sukhdev Bihar</asp:ListItem>
                                <asp:ListItem Value="Surya Nagar">Surya Nagar</asp:ListItem>
                                <asp:ListItem Value="Tugalkabad">Tugalkabad</asp:ListItem>
                                <asp:ListItem Value="Vaishali">Vaishali</asp:ListItem>
                                <asp:ListItem Value="Vasundhara">Vasundhara</asp:ListItem>
                                <asp:ListItem Value="Vasundhara En.">Vasundhara En.</asp:ListItem>
                                <asp:ListItem Value="Vijay Nagar">Vijay Nagar</asp:ListItem>
                                <asp:ListItem Value="Vinod Nagar">Vinod Nagar</asp:ListItem>
                                <asp:ListItem Value="West Delhi">West Delhi</asp:ListItem>
                                <asp:ListItem Value="Yojna Vihar">Yojna Vihar</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Perm Address<br />
                            <asp:TextBox MaxLength="45" ID="txtPerm_Address" runat="server" placeholder="PERM ADDRESS" /></td>
                        <td>Ref Src<br />
                            <asp:DropDownList ID="ddRef_Src" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Announcement">Announcement</asp:ListItem>
                                <asp:ListItem Value="Campus Group">Campus Group</asp:ListItem>
                                <asp:ListItem Value="Family">Family</asp:ListItem>
                                <asp:ListItem Value="Festival">Festival</asp:ListItem>
                                <asp:ListItem Value="Friend">Friend</asp:ListItem>
                                <asp:ListItem Value="ISK Devotee">ISK Devotee</asp:ListItem>
                                <asp:ListItem Value="Life Member">Life Member</asp:ListItem>
                                <asp:ListItem Value="Media">Media</asp:ListItem>
                                <asp:ListItem Value="Mid Day Meal">Mid Day Meal</asp:ListItem>
                                <asp:ListItem Value="Pamphlet">Pamphlet</asp:ListItem>
                                <asp:ListItem Value="Preaching">Preaching</asp:ListItem>
                                <asp:ListItem Value="Relatives">Relatives</asp:ListItem>
                                <asp:ListItem Value="School Trip">School Trip</asp:ListItem>
                                <asp:ListItem Value="Spiritual Inclination">Spiritual Inclination</asp:ListItem>
                                <asp:ListItem Value="Spiritual Literature">Spiritual Literature</asp:ListItem>
                                <asp:ListItem Value="Temple">Temple</asp:ListItem>
                                <asp:ListItem Value="Tourism">Tourism</asp:ListItem>
                                <asp:ListItem Value="Vrindavan">Vrindavan</asp:ListItem>
                                <asp:ListItem Value="WebSite">WebSite</asp:ListItem>
                                <asp:ListItem Value="World Famous">World Famous</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>RefBy<br />
                            <asp:TextBox MaxLength="45" ID="RefBy" runat="server" placeholder="REF BY" /></td>
                        <td>Relation<br />
                            <asp:DropDownList ID="ddRelation" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Brother">Brother</asp:ListItem>
                                <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                <asp:ListItem Value="Father">Father</asp:ListItem>
                                <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                <asp:ListItem Value="Self">Self</asp:ListItem>
                                <asp:ListItem Value="Sister">Sister</asp:ListItem>
                                <asp:ListItem Value="Son">Son</asp:ListItem>
                                <asp:ListItem Value="Wife">Wife</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Ext Id<br />
                            <asp:TextBox MaxLength="45" ID="txtExt_Id" runat="server" placeholder="EXT ID" /></td>
                        <td>FamilyId<br />
                            <asp:TextBox MaxLength="45" ID="txtFamilyId" runat="server" placeholder="FAMILY ID" /></td>
                        <td>
                            <br />
                            <asp:Button CssClass="btn btn-sm btn-success" Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" />
                            <asp:Button CssClass="btn btn-sm btn-primary" Text="Reset" ID="btnReset" OnClick="btnReset_Click" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblSave" runat="server" /></td>
                    </tr>
                    <%--<tr>
                        <td colspan="4">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            
                        </td>
                    </tr>--%>
                </table>
            </td>
            <td style="vertical-align: top; width: 300px"></td>
        </tr>
    </table>



</asp:Content>

