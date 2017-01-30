var React = require('react');
var SurveyStarRating = require('./SurveyStarRating');

const titleText = {
    fontSize: 15,
    fontWeight: 'bold',
    paddingTop: '13px'
}
const column = {
    paddingRight: '0px'
}


var SurveyForm = React.createClass({
    handleChange: function (fieldId, value) {

        var questionnumber = fieldId.split('-')[0];
        var starnumber = fieldId.split('-')[1];

        var newState = {};
        newState[fieldId] = value;
        newState["question-" + questionnumber] = value;
        this.setState(newState);
    },
    handleTextAreaChange: function (event) {
        var newState = {};
        newState["question-" + event.target.id] = event.target.value;
        this.setState(newState);
    },
    handleSubmit: function (e) {

        e.preventDefault();
        var surveyobject = [];
        for (var x = 1 ; x < 6; x++) {
            var question = "question-" + x;
            if (this.state[question] != null) {
                surveyobject.push({
                    "question": x,
                    "answer": this.state[question]
                })
            }
        }

        var data = {
            model: {
                CompanyId: this.props.CompanyId,
                BeaconId: this.props.BeaconId,
                Guid: this.props.Guid,
                SurveyList: surveyobject
            }
        }

        var xhr = new XMLHttpRequest();
        xhr.open('post', '/survey/new', true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    var results = JSON.parse(xhr.responseText);
                    window.location = results["RedirectTo"];
                } else {
                    window.location = "/Survey/Thanks";
                }
            }
        }.bind(this);
        xhr.send(JSON.stringify(data));
    },
    render () {
        var count = 1;
        return (
        <div>
            <div className="row">
                <img style={{ paddingBottom: '3px',padding: '0px 0px 0px 0px',margin:'0px auto', height: '175px' }} src={'/Content/bodyandpolefulllogowhite.png'} alt="boohoo" className="img-responsive" />
            </div>
            <div className="row" style={{ paddingTop: '0px' , marginTop: '10px' }}>
                <div className="col-xs-12" style={{paddingRight: '0px' , paddingLeft: '0px' }}>
                    <h5 style={{fontWeight: '900' , textAlign:'center', margin: '0px 0px 0px 0px' , paddingTop: '16px' , paddingBottom:'8px'}}>
                        Please rate each of the following
                    </h5>
                </div>
            </div>
            <div className="row" style={{ marginTop: '20px' }}>
              <div className="col-xs-1"></div>
              <div className="col-xs-10">
                  <div>
                      <form method="POST" onSubmit={this.handleSubmit }>
                            <SurveyStarRating question="Satisfaction" raitingid="1" backgroundshadow='white' onChange={this.handleChange} />
                            <SurveyStarRating question="Staff" raitingid="2" backgroundshadow='white' onChange={this.handleChange} />
                            <SurveyStarRating question="Found It!" raitingid="3" backgroundshadow='white' onChange={this.handleChange} />
                               <div className="row" style={{
                                                    borderRadius: '10px' ,
                                                    paddingTop: '10px' ,
                                                    paddingBottom: '10px' ,
                                                    margin: '0px auto' ,
                                                    maxWidth: '325px' ,
                                                    border: '1px solid black' ,
                                                    background: 'white'
                                                    }}>
                                    <div className="col-xs-12" style={{paddingTop:'30px'}}>
                                        <textarea name="TextComment" id="5" className="input-borderbottom" onChange={this.handleTextAreaChange} style={{ width: '100%' }} placeholder="Additional Comments" />
                                    </div>
                               </div>
                              <div className="row">
                                  <div className="col-xs-4"></div>
                                  <div className="col-xs-4" style={{textAlign:'center'}}>
                                    <button type="submit" style={{
                                            boxShadow: '3px 3px 5px #888888', height: '40px',
                                            fontWeight: '900',
                                            width: '95px',
                                            backgroundColor: 'EC2E91',
                                            color:'white',
                                            textAlign: 'center',
                                            marginTop: '30px',
                                        }} className="btn btn-submit">
                                        Submit
                                    </button>
                                  </div>
                              </div>
</form>
                  </div>
              </div>
              <div className="col-xs-1"></div>
              </div>
        </div>
        )
    }

});

module.exports = SurveyForm;