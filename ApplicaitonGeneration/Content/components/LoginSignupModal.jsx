var React = require('react');
var ReactDOM = require('react-dom');
var Modal = require('reactstrap').Modal;
var Button = require('reactstrap').Button;
var ModalHeader = require('reactstrap').ModalHeader;
var ModalBody = require('reactstrap').ModalBody;
var ModalFooter = require('reactstrap').ModalFooter;
var Col = require('reactstrap').Col;
var Form = require('reactstrap').Form;
var FormGroup = require('reactstrap').FormGroup;
var Label = require('reactstrap').Label;
var Input = require('reactstrap').Input;
var InputDetailText = require('./InputDetailText');


class LoginSignupModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            modal: false,
            UserId: 0,
        };
        this.handleTextChange = this.handleTextChange.bind(this);
        this.executeLoginRegister = this.executeLoginRegister.bind(this);
        this.handleLogin = this.handleLogin.bind(this);
        this.handleRegister = this.handleRegister.bind(this);
        this.toggle = this.toggle.bind(this);
    }
    handleTextChange(fieldId, value) {
        var newState = {};
        newState[fieldId + ""] = value;
        this.setState(newState);
    }
    toggle(event) {
        if (event != null) {
            event.preventDefault();
        }
        this.setState({
            modal: !this.state.modal
        });
    }
    executeLoginRegister(endpoiturl, formData) {
        var xhr = new XMLHttpRequest();
        xhr.open('post', endpoiturl, true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    console.log(xhr);
                    var results = JSON.parse(xhr.responseText);
                    var userId = results["UserId"];
                    if (userId > 0) {
                        this.props.onFinish(userId);
                        this.toggle(null);
                    } else {
                        alert(results["Result"]);
                    }
                } else {
                    alert('There was a problem with the request. Please contact us if problem persists.');
                }
            }
        }.bind(this);
        xhr.send(formData);
    }
    handleLogin(event) {
        event.preventDefault();
        var formData = new FormData();
        formData.append("Email", this.state["Email"]);
        formData.append("Password", this.state["Password"]);
        formData.append("__RequestVerificationToken", $('input[name="__RequestVerificationToken"]').val());

        this.executeLoginRegister("/Account/LoginModal", formData);
    }
    handleRegister(event) {
        event.preventDefault();

        var formData = new FormData();
        formData.append("Email", this.state.SignUpEmail);
        formData.append("Password", this.state.SignUpPassword);
        formData.append("ConfirmPassword", this.state.SignUpConfirmPassword);
        formData.append("__RequestVerificationToken", $('input[name="__RequestVerificationToken"]').val());

        this.executeLoginRegister("/Account/RegisterModal", formData);
    }

    render() {
        return (
      <div style={{marginLeft:'30px'}}>
        <Button onClick={this.toggle} style={{ width: '160px' }}>Save & Continue</Button>
        <Modal isOpen={this.state.modal} toggle={this.toggle} className="modalStyle">
          <ModalHeader className="modalHeaderStyle" toggle={this.toggle}>Lets keep that info saved.</ModalHeader>
          <ModalBody>
              <Form>
                  <div className="col-xs-6">
                    <div className="row">
                        <div className="col-xs-6">
                            <h3>Welcome back</h3><br />
                        </div>
                    </div>
                    <div className="row">
                          <div className="col-xs-11">
                            <InputDetailText name="Email" id="Email" value="" onChange={this.handleTextChange } />
                            <InputDetailText name="Password" id="Password" type="2" value="" onChange={this.handleTextChange } />
                          </div>
                    </div>
                    <div className="row">
                        <br /><br /><br /><br />
                        <Button color="primary" style={{ marginTop: '4px', marginLeft: '153px', width: '150px' }} onClick={this.handleLogin}>LOG IN</Button>
                    </div>
                  </div>
                  <div className="col-xs-6">
                    <div className="row">
                        <div className="col-xs-8">
                            <h3>New User? Sign up</h3><br />
                        </div>
                    </div>
                    <div className="row">
                          <div className="col-xs-11">
                            <InputDetailText name="Email" id="SignUpEmail" value="" onChange={this.handleTextChange } />
                            <InputDetailText name="Password" id="SignUpPassword" type="2" value="" onChange={this.handleTextChange } />
                            <InputDetailText name="Confirm Password" id="SignUpConfirmPassword" type="2" value="" onChange={this.handleTextChange } />
                          </div>
                    </div>
                    <div className="row">
                        <br /><br />
                        <Button color="primary" style={{ marginLeft: '153px', width: '150px' }} onClick={this.handleRegister}>CREATE</Button>
                    </div>
                  </div>
              </Form>
          </ModalBody>
          <ModalFooter>
              <div className="row" style={{paddingBottom:'25px'}}></div>
          </ModalFooter>
        </Modal>
      </div>
    );
    }
}

module.exports = LoginSignupModal;


//decideToggleOrSave(event) {
//    if (event != null) {
//        event.preventDefault();
//    }
//    if (this.state.UserId > 0) {
//        this.props.onFinish(this.state.UserId);
//    }
//    fetch('/Account/GetUserId', {
//        method: 'GET',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json',
//            'Authorization': 'Bearer access_token_here'
//        }
//    }).then(function (response) {
//        var contentType = response.headers.get("content-type");
//        if (contentType && contentType.indexOf("application/json") !== -1) {
//            return response.json().then(function (json) {
//                console.log(json);
//                var userId = parseInt(json.UserId);
//                if (userId > 0) {
//                    this.setState({ UserId: userId });
//                    this.props.onFinish(userId);
//                } else {
//                    this.toggle(null);
//                }
//            }.bind(this));
//        } else {
//            alert("Oops, something went wrong");
//        }
//    }.bind(this));
//}
//handleLogin(event) {
//    event.preventDefault();

//    var formData = new FormData();
//    formData.append("Email", this.state["Email"]);
//    formData.append("Password", this.state["Password"]);
//    formData.append("__RequestVerificationToken", $('input[name="__RequestVerificationToken"]').val());

//    var xhr = new XMLHttpRequest();
//    xhr.open('post', "/Account/LoginModal", true);
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState === XMLHttpRequest.DONE) {
//            if (xhr.status === 200) {
//                console.log(xhr);
//                var results = JSON.parse(xhr.responseText);
//                var userId = results["UserId"];
//                if (userId > 0) {
//                    this.props.onFinish(userId);
//                    this.toggle(null);
//                } else {
//                    alert('Username or Password was incorrect.');
//                }
//            } else {
//                alert('There was a problem with the request.');
//            }
//        }
//    }.bind(this);
//    xhr.send(formData);