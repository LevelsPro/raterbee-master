var React = require('react');
var ReactBootstrap = require('react-bootstrap');
var Select = require('react-select');
var LoginSignupModal = require('./LoginSignupModal');
var InputDetailText = require('./InputDetailText');
var Button = require('reactstrap').Button;
var InputDetailList = require('./InputDetailList');
var CollapsePanelLinks = require('./CollapsePanelLinks');

const inputStyle = {
    marginTop: '10px'
};
const labelStyle = {
    marginTop: '15px'
};
const appendbuttonstyle = {
    marginTop: '10px',
    display: 'block',
    fontSize: "20px"
};
const subdetails = {
    marginTop: '10px'
};

const listMiddle = {
    verticalAlign: 'middle'
};

var options = [
    { value: 'RebnyNonStabilizedLease', label: 'Rebny - Non Stabilized Lease' },
    { value: 'RebnySprinkleDisclosure', label: 'Rebny - SprinkleDisclosure' },
    { value: 'RebnyNoticeOfIntentionToSellOrLease', label: 'Rebny - Notice Of Intention To Sell Or Lease' },
    { value: 'AkamCondominiumLeaseApplication', label: 'Akam - CondominiumLeaseApplication' }
];

const DocumentSelectType = React.createClass({
    getInitialState: function () {
        return {
            value: "0"
        };
    },
    handleChange: function (selectobject) {
        this.setState({ value: selectobject });
        this.props.onChange(this.props.Id, selectobject);
    },
    render() {
        return (
            <div>
                <Select name="form-field-name"
                        value={this.state.value}
                        multi={true}
                        simpleValue={true}
                        options={options}
                        onChange={this.handleChange} />
            </div>

     )
    }
});


var SaveAndContinueButton = React.createClass({
    buttonOnSave() {
        //console.log(this.props.userid);
        this.props.onSave(this.props.userid);
    },
    modalOnFinish(userid) {
        this.props.onSave(userid);
    },
    render() {
        if (this.props.userid > 0) {
            //console.log("button");
            return (<span style={{marginLeft: '30px' }}>
                        <Button onClick={this.buttonOnSave} style={{ width: '160px'} }>Save & Continue</Button>
                    </span>);
        } else {
            //console.log("LoginSignupModal");
            return (<LoginSignupModal onFinish={this.modalOnFinish } />);
        }
    }
});

function GenerateInputValues(grouptype, state) {
    var parameters = "";
    var groupcount = state[grouptype + "detail" + "count"].length;
    for (var position = 0; position < groupcount; position++) {
        state[grouptype + "detaillist"].map(function (detailgroup) {
            parameters += "&" + state[detailgroup.Item + "detail"].map(function (object) {
                var key = grouptype + detailgroup.Item + "detail" + position + object;
                var value = (state[key + ""] == null) ? " " : state[key + ""];
                return object + "=" + value.trim()
            }).join("&");
        });
    }
    return (parameters);
}


function GenerateParameters(grouptype, state) {
    var value = "";
    var key = "";
    var parameters = "";
    var groupcount = state[grouptype + "detail" + "count"].length;
    for (var pos = 0; pos < groupcount; pos++) {
        var position = state[grouptype + "detail" + "count"][pos];
        if (grouptype != "client") {
            // This groups with no panel
            key = grouptype + "detail" + position + "Id";
            value = (state[key] != undefined) ? state[key] : 0;
            parameters += "&" + "Id=" + value;
        }
        state[grouptype + "detaillist"].map(function (detailgroup) {
            key = grouptype + detailgroup.Item + "detail" + position + "Id";
            //console.log(key + " == " + state[key]);
            if (detailgroup.Item == "landlordcontact") {                                            // TODO = special case
                // One Object split into more than 1 collapse (UserLandlord)
                key = grouptype + "landlorddetail" + position + "Id";
                value = (state[key] != undefined) ? state[key] : 0;
                parameters += "&Id=" + value;
            }
            key = grouptype + detailgroup.Item + "detail" + position + "Id";
            value = (state[key] != undefined) ? state[key] : 0;
            parameters += "&" + "Id=" + value;  // Save ID for update action in controller
            parameters += "&" + state[detailgroup.Item + "detail"].map(function (object) {
                key = grouptype + detailgroup.Item + "detail" + position + object;
                value = (state[key + ""] == null) ? " " : state[key + ""];
                return object + "=" + value.trim()
            }).join("&");
        });
    }
    return (parameters);
}

function GenerateDetailCount(array) {
    var skipfirst = true;
    var countlist = [0];
    array.map(function (number) {
        if (skipfirst) {
            skipfirst = false; // Skip first cause array length 1 alrdy exists.
        } else {
            countlist = countlist.concat(countlist.length);
        }
    });
    return countlist;
}

var RealtorClientForm = React.createClass({
    componentWillMount: function () {
        // Open first item in each pannel
        this.state.tabgroups.map(function (tabElement) {
            var grouptype = tabElement.Item;
            this.handleShowDetails(grouptype, this.state[grouptype + "detaillist"][0].Item, 0);
            this.handlePanelChange(grouptype, 0);
        }, this);

        // Save Id for each element loaded from DB
        var pos = 0;
        var loaded = 0;
        var keyInput = {};
        keyInput["realtorclientid"] = this.props.realtorclient.Id;
        keyInput["clientdetail0Id"] = this.props.realtorclient.ContactInfo.Id;
        this.state.tabgroups.map(function (tabElement) {
            for (pos = 0; pos < this.state[tabElement.Item + "detailcount"].length; pos++) {
                var value = 0;
                var stateref = tabElement.Item + "detail" + pos + "Id";
                if (this.props.realtorclient[tabElement.ServerObj] != null && this.props.realtorclient[tabElement.ServerObj][pos] != null) {
                    value = this.props.realtorclient[tabElement.ServerObj][pos].Id;
                }
                keyInput[stateref] = value;

                if (tabElement.Item == "home") {
                    // Landlord has it's own object (UserLandlord) with multiple references so an extra Id is added here
                    stateref = "home" + "landlord" + "detail" + pos + "Id";                                                         // TODO = special case
                    if (this.props.realtorclient[tabElement.ServerObj] != null && this.props.realtorclient[tabElement.ServerObj][pos] != null) {
                        value = this.props.realtorclient[tabElement.ServerObj][pos].Landlord.Id;
                    }
                    keyInput[stateref] = value;
                }
                this.state[tabElement.Item + "detaillist"].map(function (detailgroup) {
                    var variables = detailgroup.ServerObj.split(".");
                    if (variables.length == 1) {
                        value = this.props.realtorclient[tabElement.ServerObj][pos][variables[0]];
                    } else if (variables.length == 2) {
                        value = this.props.realtorclient[tabElement.ServerObj][pos][variables[0]][variables[1]];
                    } else {
                        alert("WARNING - have not covered this case. ");
                    }
                    //console.log(variables);
                    //console.log(value);
                    keyInput["serverloaded" + loaded++] = value;
                }, this);
            }
        }, this);

        this.setState(keyInput, function () { return; });
        console.log(this.props.realtorclient);
        console.log(this.state);
        console.log(keyInput);
    },
    getInitialState() {
        //console.log(this.props.realtorclient);

        var homecount = GenerateDetailCount(this.props.realtorclient["Homes"]);
        var employmentcount = GenerateDetailCount(this.props.realtorclient["Employments"]);
        var bankcount = GenerateDetailCount(this.props.realtorclient["Banks"]);
        var personalcount = GenerateDetailCount(this.props.realtorclient["PersonalReferences"]);
        var professionalcount = GenerateDetailCount(this.props.realtorclient["ProfessionalReferences"]);
        return {
            key: 1,
            clientdetailcount: [0],
            homedetailcount: homecount,
            employmentdetailcount: employmentcount,
            bankdetailcount: bankcount,
            personaldetailcount: personalcount,
            professionaldetailcount: professionalcount,

            // List of tab elements
            // Item - entity needs to have a detaillist below
            // Label - Is the display on the (+Add ) buttons, and on the tabs, and on the collapsepanellinks on left
            // ServeryObj - is the reference to the object sent from server. at the moment the object is RealtorClient
            tabgroups: [
                {
                    Item: 'home',
                    Label: 'Home',
                    ServerObj: "Homes"
                },
                {
                    Item: 'employment',
                    Label: 'Employment',
                    ServerObj: "Employments"
                },
                {
                    Item: 'bank',
                    Label: 'Bank',
                    ServerObj: "Banks"
                },
                {
                    Item: 'personal',
                    Label: 'Personal Ref.',
                    ServerObj: "PersonalReferences"
                },
                {
                    Item: 'professional',
                    Label: 'Professional Ref.',
                    ServerObj: "ProfessionalReferences"
                }
            ],

            //  ----- List os Realtor Objects (with exception of clientdetail)
            //
            clientdetaillist: [
                { Item: 'contact', Label: 'Contact Info' }
            ],
            homedetaillist: [
                { Item: 'address', Label: 'Address', ServerObj: 'Address' },
                { Item: 'lease', Label: 'Lease', ServerObj: 'Lease' },
                { Item: 'landlordcontact', Label: 'Landlord Contact', ServerObj: 'Landlord.ContactInfo' },
                { Item: 'landlordaddress', Label: 'Landlord Address', ServerObj: 'Landlord.Address' }
            ],
            employmentdetaillist: [
                { Item: 'occupation', Label: 'Occupation', ServerObj: "Occupation" },
                { Item: 'address', Label: 'Address', ServerObj: "Address" },
                { Item: 'salary', Label: 'Salary', ServerObj: "Salary" },
                { Item: 'supervisortitle', Label: 'Supervisor Title', ServerObj: "Supervisor" },
                { Item: 'supervisorcontact', Label: 'Supervisor Contact', ServerObj: "Supervisor.ContactInfo" },
            ],
            bankdetaillist: [
                { Item: 'bankaccount', Label: 'Bank Account', ServerObj: "Account" },
                { Item: 'bankaddress', Label: 'Bank Address', ServerObj: "Address" }
            ],
            personaldetaillist: [
                { Item: 'reference', Label: 'Refeerence Title', ServerObj: 'Reference' },
                { Item: 'contact', Label: 'Reference Contact', ServerObj: 'Reference.ContactInfo' }
            ],
            professionaldetaillist: [
                { Item: 'reference', Label: 'Refeerence Title', ServerObj: 'Reference' },
                { Item: 'contact', Label: 'Reference Contact', ServerObj: 'Reference.ContactInfo' }
            ],

            //----------------------------------Everything that has an ID needs a list here---------------------------------
            // Names must match variable name within objet
            contactdetail: ['FirstName', 'LastName', 'Email', 'PhoneNumber', 'Education'],
            addressdetail: ['AddressNumber', 'Street', 'City', 'State', 'Zip', 'ApartmentNumber', 'ApartmentFloor', 'Neighborhood'],
            leasedetail: [{ Name: 'StartDate', Type: '1' }, { Name: 'EndDate', Type: '1' }, 'MonthlyRent', 'SecurityDeposit', 'GarageFee', 'UtilitiesIncludedInRent'],

            landlordcontactdetail: ['FirstName', 'LastName', 'Email', 'PhoneNumber'],
            landlordaddressdetail: ['AddressNumber', 'Street', 'City', 'State', 'Zip', 'ApartmentNumber', 'ApartmentFloor'],

            occupationdetail: ['Title', 'CompanyName', { Name: 'StartDate', Type: '1' }, { Name: 'EndDate', Type: '1' }],
            salarydetail: ['AnnualSalary', 'Bonus', 'OtherIncomeSource', 'OtherIncomeAmount', 'TotalAnnualIncome'],
            supervisortitledetail: ['Title'],
            supervisorcontactdetail: ['FirstName', 'LastName', 'Email', 'PhoneNumber'],

            bankaccountdetail: ['AccountName', 'AccountType'],
            bankaddressdetail: ['AddressNumber', 'Street', 'City', 'State', 'Zip'],
            bankcpadetail: ['FirstName', 'LastName', 'Email', 'PhoneNumber', 'CompanyName'],

            referencedetail: ['Relationship']

            //guarantordetail: ['Name', 'RelationToApplicant', 'PhoneNumber', 'EmploymentStatus'],
            //guarantorbank: ['BankName', 'Address', 'AccountType'],
            //guarantoremployment: ['Occupation', 'CompanyName', 'EmploymentStart', 'EmploymentEnd', 'AnnualSalary', 'Bonus', 'CompanyAddress', 'SupervisorName', 'SupervisorPhone', 'OtherIncomeSource', 'OtherIncomeAmount', 'TotalAnnualIncome'],
            //nameageoccupant: ["Name", "Age"],
            //petdetail: ["HavePets", "Breed"]
        }
    },
    onSave: function (userId) {
        if (userId > 0) {
            this.handleSubmit("save");
        }
    },
    handleGenerate: function () {
        this.handleSubmit("generate");
    },
    handleSave: function () {
        this.handleSubmit("save");
    },
    handleSubmit: function (action) {
        if (action == "generate" && (this.state.documenttypes == "" || this.state.documenttypes == null)) {
            alert("Please select documents to generate.");
            return;
        }
        console.log(this.state);

        var parameters = "actiontype=" + action;
        parameters += "&" + "documenttypes=" + this.state.documenttypes;
        parameters += "&" + "client" + "count=" + this.state["client" + "detailcount"].length;
        this.state.tabgroups.map(function (tabElement) {
            parameters += "&" + tabElement.Item + "count=" + this.state[tabElement.Item + "detailcount"].length;
        }, this);

        parameters += "&" + "Id=" + this.state["realtorclientid"];                                          // TODO = special case
        parameters += GenerateParameters("client", this.state);
        this.state.tabgroups.map(function (tabElement) {
            parameters += GenerateParameters(tabElement.Item, this.state);
        }, this);

        console.log(parameters);

        window.location = "/realtordocument/new" + "?" + parameters;
    },
    appendDetails: function (e) {
        var keyInput = {};
        var stateref = e + "detailcount";
        keyInput[stateref] = this.state[stateref].concat(this.state[stateref].length);
        this.handleInitNewDetail(e, this.state[stateref].length - 1);
        this.setState(keyInput, function () { return; });
        console.log(this.state);
    },
    handleRemovePanel: function (grouptype, index) {
        this.removePanel(grouptype, index);
        //var Id = this.state[grouptype + "detail" + index + "Id"];
        //if (Id > 0) {
        //    var formData = new FormData();
        //    formData.append("id", index);
        //    formData.append("type", this.props.serverobj);
        //    formData.append("__RequestVerificationToken", $('input[name="__RequestVerificationToken"]').val());
        //    var xhr = new XMLHttpRequest();
        //    xhr.open('post', '/RealtorDocument/DeleteObject', true);
        //    xhr.onreadystatechange = function () {
        //        if (xhr.readyState === XMLHttpRequest.DONE) {
        //            if (xhr.status === 200) {
        //                alert(results["Result"]);
        //                this.removePanel(grouptype, index);
        //            } else {
        //                alert('There was a problem with the request. Please contact us if problem persists.');
        //            }
        //        }
        //    }.bind(this);
        //    xhr.send(formData);
        //} else {
        //    this.removePanel(grouptype, index);
        //}
    },
    removePanel: function (grouptype, index){
        var keyInput = {};
        var stateref = grouptype + "detailcount";
        keyInput[stateref] = this.state[stateref].filter((_, i) => i !== index);
        this.setState(keyInput, function () { return; });
    },
    handlePanelChange: function (grouptype, index) {
        var open = this.state[grouptype + "detailpanel" + index];
        for (var i = 0; i < this.state[grouptype + "detailcount"].length; i++) {
            var key = grouptype + "detailpanel" + i;
            var newInput = {};
            newInput[key + ""] = false;
            this.setState(newInput);
        }
        var keyInput = {};
        keyInput[grouptype + "detailpanel" + index] = true;
        this.setState(keyInput);
        this.handleShowDetails(grouptype, this.state[grouptype + "detaillist"][0].Item, index);
    },
    handleInitNewDetail: function (grouptype, index) {
        this.handleHideDetails(grouptype);
        this.handleShowDetails(grouptype, this.state[grouptype + "detaillist"][0].Item, index);
        this.handlePanelChange(grouptype, index);
    },
    handleFieldChange: function (fieldId, value) {
        var newState = {};
        newState[fieldId + ""] = value;
        this.setState(newState);
    },
    handleTabSelect(key) {
        this.setState({ key });
    },
    handleHideDetails: function (grouptype) {
        //console.log(grouptype + "detail" + "count = " + this.state[grouptype + "detail" + "count"]);
        var newInputs = {};
        var groupcount = this.state[grouptype + "detail" + "count"].length;
        for (var position = 0; position < groupcount; position++) {
            this.state[grouptype + "detaillist"].map(function (detailgroup) {
                newInputs[grouptype + detailgroup.Item + "detailpanel" + position] = false;
            });
        }
        this.setState(newInputs);
    },
    handleShowDetails(grouptype, detailtype, index) {
        this.handleHideDetails(grouptype);
        var newInputs = {};
        newInputs[grouptype + detailtype + "detailpanel" + index] = true;
        this.setState(newInputs);
        //console.log(grouptype + detailtype + "detailpanel" + index + "= true");
    },
    render: function () {
        var count = 0;
        var tabindex = 2;           // TabIndex starts at 2 since 1, is static and outlined below
        var SaveContinueButton;
        var tabhtml = [];
        this.state.tabgroups.map(function (tabElement) {
            console.log("_____Start TAb" + tabElement.Item);
            tabhtml.push(<ReactBootstrap.Tab key={tabElement.Item + "detail"} eventKey={tabindex++} title={tabElement.Label} style={labelStyle }>
                <div className="row" style={{ marginTop: '10px' } }>
                    <div className="col-sm-2">
                        <br />
                        <ReactBootstrap.Button onClick={ () => this.appendDetails(tabElement.Item) }>+ Add {tabElement.Label} </ReactBootstrap.Button>
                        <br /><br />
                        {this.state[tabElement.Item + "detailcount"].map(function (index) {
                            return (<CollapsePanelLinks key={tabElement.Item + "detaillinks" + index}
                                                        index={index}
                                                        grouptype={tabElement.Item}
                                                        grouplabel={tabElement.Label}
                                                        detaillist={this.state[tabElement.Item + "detaillist"]}
                                                        handlePanelChange={this.handlePanelChange}
                                                        handleShowDetails={this.handleShowDetails}
                                                        handleRemovePanel={this.handleRemovePanel} />);
                        }, this)}
                    </div>
                <div className="col-sm-1"></div>
                <div className="col-sm-8">
                    <br /><br />
                    {this.state[tabElement.Item + "detailcount"].map(function (index) {
                        var listhtml = [];
                        console.log("-------- " + tabElement.Item + " --- " + index + "--------count = " + count);
                        this.state[tabElement.Item + "detaillist"].map(function (obj) {
                            console.log(this.state["serverloaded" + count]);
                            listhtml.push(
                            <ReactBootstrap.Collapse in={this.state[tabElement.Item + obj.Item + "detailpanel" + index]} key={tabElement.Item + obj.Item + "detailpanel" + index}>
                                <div>
                                <h3>{obj.Label} Information</h3>
                                <InputDetailList data={this.state["serverloaded" + count++]}
                                                 detaillist={this.state[obj.Item + "detail"]}
                                                 detailtype={tabElement.Item + obj.Item + "detail"}
                                                 count={index}
                                                 onChange={this.handleFieldChange} />
                                </div>
                            </ReactBootstrap.Collapse>);
                        }, this);
                        return (<div key={tabElement.Item + "detaillist" + index }>{listhtml}</div>)
                    }, this)}
                </div>
                </div>
            </ReactBootstrap.Tab>);
        }, this);
        //<div className="row" style={{ paddingBottom: "35px" }}>
        //    <div className="col-sm-8"><DocumentSelectType authenticated={this.props.authenticated} id="documenttypes" value={this.state["documenttypes"]} onChange={this.handleFieldChange } /></div>
        //    <div className="col-sm-2" style={{ marginTop: "28px", paddingRight: "0px" } }><Button onClick={this.handleGenerate}>Generate Application</Button></div>
        //    <div className="col-sm-2" style={{ paddingLeft: "0px", marginTop: "28px" } }><SaveAndContinueButton userid={this.props.userid} onSave={this.onSave } /></div>
        //</div>
        return (
            <div>
                <div className="row">
                    <div className="col-xs-4">
                        <h1 className="">
                            Application Generation
                        </h1>
                    </div>
                    <div className="col-xs-8" style={{paddingTop: '25px'}}>
                        <div className="row">
                            <div className="col-xs-8">
                                <DocumentSelectType id="documenttypes"
                                                    authenticated={this.props.authenticated}
                                                    value={this.state["documenttypes"]}
                                                    onChange={this.handleFieldChange } />
                            </div>
                            <div className="col-xs-4">
                                <Button onClick={this.handleGenerate} style={{ width: '160px' }}>Generate Paperwork</Button>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div className="row">
                    <div className="col-xs-3" style={{ width:"345px", marginTop: "0px" } }>
                    </div>
                    <div className="col-xs-4">
                        <div className="row">
                            <div className="col-xs-6" style={{paddingTop: '16px'}}>
                                <span style={{fontSize: '14px'}}>Email your agent entered info.</span>
                            </div>
                            <div className="col-xs-6" style={{ marginTop: "0px"} }>
                                <span style={{marginLeft: '30px' }}>
                                    <Button onClick={this.buttonOnSave} style={{ width: '160px' }}>Send Email</Button>
                                </span>
                            </div>
                        </div>
                        <hr style={{marginTop:'0px', borderColor:'#afafaf !important'}} />
                    </div>
                    <div className="col-xs-4">
                        <div className="row">
                            <div className="col-xs-6" style={{paddingTop: '16px'}}>
                                <span style={{fontSize: '14px'}}>Save and Continue for later.</span>
                            </div>
                            <div className="col-xs-6" style={{ marginTop: "0px"} }>
                                <SaveAndContinueButton userid={this.props.userid} onSave={this.onSave } />
                            </div>
                        </div>
                        <hr style={{ marginTop: '0px', borderColor: '#afafaf !important' }}/>
                    </div>
                </div>
                <ReactBootstrap.Tabs style={labelStyle} activeKey={this.state.key} onSelect={this.handleTabSelect} id="controlled-tab-example">
                    <ReactBootstrap.Tab eventKey={1} title="Personal Info" style={labelStyle}>
                        <div className="row" style={{ marginTop: '10px' }}>
                            <div className="col-sm-2">
                                <br />
                                <div className="row">
                                    <div className="col-sm-6" style={inputStyle}>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-1"></div>
                            <div className="col-sm-8">
                                <ReactBootstrap.Collapse in={true} key={"client" + "contact" + "detailpanel" + "0"}>
                                    <div>
                                        <h3>Personal Information</h3>
                                        <InputDetailList data={this.props.realtorclient.ContactInfo}
                                                         detaillist={this.state.contactdetail}
                                                         detailtype={"client" + "contact" + "detail"}
                                                         count="0"
                                                         onChange={this.handleFieldChange} />
                                    </div>
                                </ReactBootstrap.Collapse>
                            </div>
                        </div>
                    </ReactBootstrap.Tab>
                    {tabhtml}
                </ReactBootstrap.Tabs>
            </div>
    );
    }
});


var Document = React.createClass({
    render: function () {
        return (
            <div className="ApplicationGenerationForm">
                <div className="row">
                    <div className="col-sm-1"></div>
                    <div className="col-sm-10">
                        <RealtorClientForm authenticated={this.props.Authenticated} userid={this.props.UserId} realtorclient={this.props.RealtorClient} />
                    </div>
                    <div className="col-sm-1"></div>
                </div>
            </div>
      );
    }
});


module.exports = Document;
