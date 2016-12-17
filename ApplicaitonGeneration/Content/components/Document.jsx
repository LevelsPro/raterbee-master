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
            return (<span style={{ marginLeft: '30px' }}>
                    <Button onClick={this.buttonOnSave} style={{ width: '160px'} }>Save & Continue</Button>
            </span>);
        } else {
            //console.log("LoginSignupModal");
            return (<LoginSignupModal onFinish={this.modalOnFinish } />);
        }
    }
});

function GenerateParameters(grouptype, state, index) {
    var value = "";
    var key = "";
    var newObj = [];
    var groupcount = state[grouptype + "list"].length;
    for (var pos = 0; pos < groupcount; pos++) {
        if (index == pos) continue;                     // Skipp index (Used to remove panels)
        var curObj = {};
        if (grouptype != "client") {
            // This groups with no panel
            key = grouptype + "detail" + "Id" + pos;
            value = (state[key] != undefined) ? state[key] : 0;
            curObj["Id"] = value;
            console.log(grouptype + " - " + "Id" + " - " + pos + " - " + value);
        }
        state[grouptype + "detaillist"].map(function (detailgroup) {
            var variables = detailgroup.ServerObj.split(".");
            if (variables[0] == "Landlord" && curObj["Landlord"] == null) {                                                 // TODO = special case
                // Only enter this once
                // One Object split into more than 1 collapse (UserLandlord)
                key = grouptype + "landlorddetail" + "Id" + pos;
                value = (state[key] != undefined) ? state[key] : 0;
                curObj[variables[0]] = {};
                curObj[variables[0]]["Id"] = value;
            }
            if (curObj[variables[0]] == null) {
                curObj[variables[0]] = {};
            }
            key = grouptype + detailgroup.Item + "detail" + "Id" + pos;
            value = (state[key] != undefined) ? state[key] : 0;

            console.log(detailgroup.ServerObj + " - " + "Id" + " - " + pos + " - " + value);
            if (variables.length == 1) {
                curObj[variables[0]]["Id"] = value;
            } else if (variables.length == 2) {
                if (curObj[variables[0]][variables[1]] == null) {
                    curObj[variables[0]][variables[1]] = {};
                }
                curObj[variables[0]][variables[1]]["Id"] = value;
            } else {
                alert("WARNING - have not covered this case. ");
            }

            state[detailgroup.Item + "detail"].map(function (obj) {
                var object = obj;
                if (obj.Name) { obj = obj.Name; }
                key = grouptype + detailgroup.Item + "detail" + object + pos;
                value = (state[key + ""] == null) ? " " : state[key + ""];
                console.log(detailgroup.ServerObj + " - " + object + " - " + pos + " - " + value);
                if (variables.length == 1) {
                    curObj[variables[0]][object] = value.trim();
                } else if (variables.length == 2) {
                    curObj[variables[0]][variables[1]][object] = value.trim();
                } else {
                    alert("WARNING - have not covered this case. ");
                }
            });
        });
        if (grouptype != "client") {
            newObj.push(curObj);
        } else {
            newObj = curObj;
        }
    }
    return newObj;
}


var RealtorClientForm = React.createClass({
    componentWillMount: function () {
        // Open first item in each pannel
        var newInputs = {};
        this.state.tabgroups.map(function (tabElement) {
            var grouptype = tabElement.Item;
            for (var pos = 0; pos < this.state[tabElement.Item + "list"].length; pos++) {
                newInputs[tabElement.Item + "detail" + "Id" + pos] = this.state[tabElement.Item + "list"][pos]["Id"];

                if (tabElement.Item == "home") {                                                 // TODO = special case
                    var key = tabElement.Item + "landlorddetail" + "Id" + pos;
                    newInputs[key] = this.state[tabElement.Item + "list"][pos]["Landlord"]["Id"];
                }
            }
        }, this);
        this.setState(newInputs);

        console.log(this.props.realtorclient);
        console.log(this.state);
    },
    getInitialState() {
        var clientlist = [];
        clientlist.push(this.props.realtorclient.ContactInfo);
        return {
            key: 1,
            clientlist: clientlist,
            homelist: this.props.realtorclient.Homes,
            employmentlist: this.props.realtorclient.Employments,
            banklist: this.props.realtorclient.Banks,
            personallist: this.props.realtorclient.PersonalReferences,
            professionallist: this.props.realtorclient.ProfessionalReferences,

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
                { Item: 'contact', Label: 'Contact Info', ServerObj: 'ContactInfo' }
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
        var realtorClient = {};
        realtorClient = GenerateParameters("client", this.state, -1);
        realtorClient["Id"] = this.props.realtorclient.Id;
        this.state.tabgroups.map(function (tabElement) {
            realtorClient[tabElement.ServerObj] = GenerateParameters(tabElement.Item, this.state, -1);
        }, this);

        console.log(realtorClient);
        console.log(this.state);

        var formData = {
            "actiontype": action,
            "documenttypes": this.state.documenttypes,
            realtorclient: realtorClient
        }
        var xhr = new XMLHttpRequest();
        xhr.open('post', '/realtordocument/new', true);
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        xhr.setRequestHeader("__RequestVerificationToken", $('input[name="__RequestVerificationToken"]').val());
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    window.location = "/Client/Index";
                } else {
                    //console.log(xhr);
                    alert('There was a problem with the request. Please contact us if problem persists.');
                }
            }
        }.bind(this);
        xhr.send(JSON.stringify(formData));

        //JSON.stringify(realtorclient)
        //var token = $('input[name="__RequestVerificationToken"]').val();
        //fetch('/realtordocument/new', {
        //    method: 'POST',
        //    headers: {
        //        __RequestVerificationToken: token,
        //        'Content-Type': 'application/json; charset=utf-8'
        //    },
        //    body: JSON.stringify({
        //        actiontype: action,
        //        documenttypes: this.state.documenttypes,
        //        realtorclient: realtorClient
        //    }),
        //}).then(function (response) {
        //    if (response.ok) {
        //        //console.log(response);
        //    } else {
        //        alert('Network response was not ok.');
        //    }
        //}).catch(function (error) {
        //    console.log('There has been a problem with your fetch operation: ' + error.message);
        //});
        //window.location = "/realtordocument/new" + "?" + parameters;
    },
    appendDetails: function (e) {
        var newObj = {};
        var keyInput = {};
        var stateref = e + "list";
        this.state[e + "detaillist"].map(function (detailgroup) {
            var variables = detailgroup.ServerObj.split(".");
            if (variables.length == 1) {
                newObj[variables[0]] = {};
            } else if (variables.length == 2) {
                if (newObj[variables[0]] == undefined) {
                    newObj[variables[0]] = {};
                }
                newObj[variables[0]][variables[1]] = {};
            } else {
                alert("WARNING - have not covered this case. ");
            }
        });
        //console.log(newObj);
        keyInput[stateref] = this.state[stateref].concat(newObj);
        this.setState(keyInput, function () { return; });

        //this.handleInitNewDetail(e, this.state[stateref].length);
        //console.log(this.state);
    },
    handleRemovePanel: function (grouptype, index) {
        var key = "";
        var value = "";
        var keyInput = {};
        var stateref = grouptype + "list";
        var i = 0;
        for (var pos = 0; pos < this.state[grouptype + "list"].length; pos++) {
            // Skip index and shit all variables forward
            if (pos == index) continue;
            this.state[grouptype + "detaillist"].map(function (detailgroup) {
                var variables = detailgroup.ServerObj.split(".");
                if (variables[0] == "Landlord") {                                                 // TODO = special case
                    key = grouptype + "landlorddetail" + "Id";
                    value = (this.state[key + pos] != undefined) ? this.state[key + pos] : 0;
                    keyInput[key + i] = value;
                }
                key = grouptype + detailgroup.Item + "detail" + "Id";
                value = (this.state[key + pos] != undefined) ? this.state[key + pos] : 0;
                keyInput[key + i] = value;

                this.state[detailgroup.Item + "detail"].map(function (obj) {
                    var object = obj;
                    if (obj.Name) { obj = obj.Name; }
                    key = grouptype + detailgroup.Item + "detail" + object;
                    value = (this.state[key + pos] == null) ? " " : this.state[key + pos];
                    keyInput[key + i] = value;
                }, this);
            }, this);
            i++;
        }
        // Update all input boxes to reflect removing index
        keyInput[stateref] = GenerateParameters(grouptype, this.state, index);
        this.setState(keyInput, function () { return; });
    },
    handlePanelChange: function (grouptype, index, open) {
        // Toggles showing details on left ("Home" is a panel)
        var newInput = {};
        for (var i = 0; i < this.state[grouptype + "list"].length; i++) {
            newInput[grouptype + "detailpanel" + i] = false;
        }
        newInput[grouptype + "detailpanel" + index] = open;
        this.setState(newInput);
        if (open) {
            this.handleShowDetails(grouptype, this.state[grouptype + "detaillist"][0].Item, index);
        } else {
            this.handleHideDetails(grouptype);
        }
    },
    handleFieldChange: function (fieldId, value) {
        var newInputs = {};
        newInputs[fieldId] = value;
        this.setState(newInputs);
    },
    handleTabSelect(key) {
        this.setState({ key });
    },
    handleHideDetails: function (grouptype) {
        // Toggle details on rgith (Address, ContactInfo, Reference)
        var newInputs = {};
        for (var position = 0; position < this.state[grouptype + "list"].length; position++) {
            this.state[grouptype + "detaillist"].map(function (detailgroup) {
                newInputs[grouptype + detailgroup.Item + "detailpanel" + position] = false;
            });
        }
        this.setState(newInputs);
    },
    handleShowDetails(grouptype, detailtype, index) {
        // Toggle details on rgith (Address, ContactInfo, Reference)
        this.handleHideDetails(grouptype);
        var key = grouptype + detailtype + "detailpanel" + index;
        var newInput = {};
        newInput[key] = true;
        this.setState(newInput);
    },
    render: function () {
        var tabindex = 2;           // TabIndex starts at 2 since 1, is static and outlined below
        var SaveContinueButton;
        var tabhtml = [];
        this.state.tabgroups.map(function (tabElement) {
            var count = 0;
            var index = 0;
            tabhtml.push(<ReactBootstrap.Tab key={tabElement.Item + "detail"} eventKey={tabindex++} title={tabElement.Label} style={labelStyle }>
                <div className="row" style={{ marginTop: '10px' } }>
                    <div className="col-sm-3">
                        <br />
                        <ReactBootstrap.Button onClick={ () => this.appendDetails(tabElement.Item) }>+ Add {tabElement.Label} </ReactBootstrap.Button>
                        <br /><br />
                        {this.state[tabElement.Item + "list"].map(function (realtorobject) {
                            var curIdx = count++;
                            return (<CollapsePanelLinks key={tabElement.Item + "detaillinks" + curIdx}
                                                        index={curIdx}
                                                        grouptype={tabElement.Item}
                                                        grouplabel={tabElement.Label}
                                                        detaillist={this.state[tabElement.Item + "detaillist"]}
                                                        handlePanelChange={this.handlePanelChange}
                                                        handleShowDetails={this.handleShowDetails}
                                                        handleRemovePanel={this.handleRemovePanel
                            } />);
                        }, this)}
                    </div>
                <div className="col-sm-1"></div>
                <div className="col-sm-8">
                    {this.state[tabElement.Item + "list"].map(function (realtorobject) {
                        var curIdx = index++;
                        var listhtml = [];
                        this.state[tabElement.Item + "detaillist"].map(function (obj) {
                            var data = {};
                            var variables = obj.ServerObj.split(".");
                            if (variables.length == 1) {
                                data = realtorobject[variables[0]];
                            } else if (variables.length == 2) {
                                data = realtorobject[variables[0]][variables[1]];
                            } else {
                                alert("WARNING - have not covered this case. ");
                            }
                            listhtml.push(
                                <ReactBootstrap.Collapse in={this.state[tabElement.Item + obj.Item + "detailpanel" + curIdx]} key={tabElement.Item + obj.Item + "detailpanel" + index}>
                                    <div>
                                    <h3>{obj.Label} Information</h3>
                                    <InputDetailList data={data}
                                                     detaillist={this.state[obj.Item + "detail"]}
                                                     detailtype={tabElement.Item + obj.Item + "detail"}
                                                     index={curIdx}
                                                     onChange={this.handleFieldChange} />
                                    </div>
                                </ReactBootstrap.Collapse>);
                        }, this);
                        return (<div key={tabElement.Item + "detaillist" + curIdx }>{listhtml}</div>)
                    }, this)}
                </div>
                </div>
            </ReactBootstrap.Tab>);
        }, this);
        //<div className="row" style={{ paddingBottom: "35px" }}>
        //    <div className="col-sm-8"><DocumentSelectType authenticated={this.props.authenticated} id="documenttypes" value={this.state["documenttypes"]} onChange={this.handleFieldChange } /></div>
        //    <div className="col-sm-2" style={{ marginTop: "28px" , paddingRight: "0px" } }><Button onClick={this.handleGenerate}>Generate Application</Button></div>
        //    <div className="col-sm-2" style={{ paddingLeft: "0px" , marginTop: "28px" } }><SaveAndContinueButton userid={this.props.userid} onSave={this.onSave } /></div>
        //        </div>
        var titleshown = (this.props.authenticated) ? "hide" : "row";
        return (
            <div>
                <div className={titleshown}>
                    <div className="col-xs-8">
                        <h1 className="headerh1">
                            Application Generation
                        </h1>
                    </div>
                    <div className="col-xs-4" style={{marginTop: '27px'}}>
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
                </div>
                <br />
                <div className="row">
                    <div className="col-xs-10">
                        <div className="row">
                            <div className="col-xs-10">
                                <DocumentSelectType id="documenttypes"
                                                    authenticated={this.props.authenticated}
                                                    value={this.state["documenttypes"]}
                                                    onChange={this.handleFieldChange } />
                            </div>
                            <div className="col-xs-2">
                                <Button onClick={this.handleGenerate} style={{ width: '160px' }}>Generate Paperwork</Button>
                            </div>
                        </div>
                    </div>
                    <div className="col-xs-2" style={{ marginTop: "0px"} }>
                        <SaveAndContinueButton userid={this.props.userid} onSave={this.onSave } />
                    </div>
                </div>
                <br />
                <br />
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
                                                         index="0"
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
