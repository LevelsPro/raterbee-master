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
        console.log(fieldId + " = " + value);
        var questionnumber = fieldId.split('-')[0];
        var starnumber = fieldId.split('-')[1];

        var newState = {};
        newState[fieldId] = value;
        newState["question-" + questionnumber] = value;
        this.setState(newState);
    },
    handleSubmit: function (e) {

        e.preventDefault();
        console.log(this.state);
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
                    console.log(xhr);
                    var results = JSON.parse(xhr.responseText);
                    window.location = results["RedirectTo"];
                } else {
                    window.location = "/Survey/Thanks";
                }
            }
        }.bind(this);
        xhr.send(JSON.stringify(data));
        //var xhr = new XMLHttpRequest();
        //xhr.open('get', this.props.submitUrl + "?" + parameters, true);
        //xhr.send();
        //window.location = this.props.submitUrl + "?" + parameters;

    },
    render () {
        var count = 1;
        return (
        <div>
            <div className="row" style={{padding:'0px 0px 0px 0px', margin:'0px 0px 0px 0px' }}>
                <div className="col-xs-6" style={{ left: '5px', padding:'0px 0px 0px 0px', margin:'0px 0px 0px 0px' }}>
                    <img style={{ height: '53px', paddingBottom: '3px' }} src={'/Content/modells-logo.png'} alt="boohoo" className="img-responsive" />
                </div>
                <div className="col-xs-6" style={{padding:'0px 0px 0px 0px', margin:'5px 0px 0px 0px'}}>
                    <div style={{ height: '41px', backgroundColor: 'black', marginLeft:'5px', marginRight:'6px'}}></div>

                </div>
             </div>
            <div className="row" style={{ paddingTop: '0px' , marginTop: '0px' }}>
                <div className="col-xs-12" style={{paddingRight: '0px' , paddingLeft: '0px' }}>
                    <h3 style={{fontWeight: '900' , backgroundColor: 'FF5733' , 
                        textAlign:'center', margin: '0px 20px 0px 20px' , paddingTop: '10px' , paddingBottom:'10px'}}>Shopping Experience</h3>
                </div>
            </div>
              <div className="row">
              <div className="col-xs-1"></div>
              <div className="col-xs-10">
                  <div>
                      <form method="POST" onSubmit={this.handleSubmit }>
                             <h5 style={{fontWeight: '900' , textAlign:'center', margin: '0px 0px 0px 0px' , paddingTop: '16px' , paddingBottom:'8px'}}>
                                 Please rate each of the following
                             </h5>
                            <SurveyStarRating question="Satisfaction" raitingid="1" backgroundshadow='#888888' onChange={this.handleChange} />
                            <SurveyStarRating question="Staff" raitingid="2" backgroundshadow='#888888' onChange={this.handleChange} />
                            <SurveyStarRating question="Found It!" raitingid="3" backgroundshadow='#888888' onChange={this.handleChange} />
                            <SurveyStarRating question="Wait to pay" raitingid="4" backgroundshadow='#888888' onChange={this.handleChange} />
                              <div className="row">
                                  <div className="col-xs-4"></div>
                                  <div className="col-xs-4">
                                    <button type="submit" style={{ boxShadow: '3px 3px 5px #888888', height: '40px', fontWeight: '900', width: '95px', backgroundColor: 'FF5733', textAlign: 'center', marginTop: '30px' }} className="btn btn-submit">Submit</button>
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