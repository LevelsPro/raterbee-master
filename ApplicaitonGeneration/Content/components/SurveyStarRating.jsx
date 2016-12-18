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


var SurveyStarRating = React.createClass({
    handleChange: function (event) {
        var Id = event.target.id;
        var Text = event.target.value;
        this.props.onChange(Id, Text);
    },
    render () {
        return (
            <div className="row" style={{ boxShadow: '3px 3px 5px #888888', borderRadius: '10px', paddingTop: '10px',paddingBottom:'10px', marginTop: '10px', background: 'lightblue'}}>
                <div className="col-xs-5" style={column}>
                    <div style={titleText} className="">{this.props.question}</div>
                </div>
                <div className="col-xs-7">
                    <div className="rating">
                        <input onChange={this.handleChange}
                               type="radio" id={this.props.raitingid + "-4"}
                               name={this.props.raitingid + "rating"}
                               value="4" />
                        <label htmlFor={this.props.raitingid + "-4"} title="Pretty good">4 stars</label>

                        <input onChange={this.handleChange}
                               type="radio" id={this.props.raitingid + "-3"}
                               name={this.props.raitingid + "rating"}
                               value="3" />
                        <label htmlFor={this.props.raitingid + "-3"} title="Meh">3 stars</label>

                        <input onChange={this.handleChange}
                               type="radio" id={this.props.raitingid + "-2"}
                               name={this.props.raitingid + "rating"}
                               value="2" />
                        <label htmlFor={this.props.raitingid + "-2"} title="Kinda bad">2 stars</label>

                        <input onChange={this.handleChange}
                               type="radio"
                               id={this.props.raitingid + "-1"}
                               name={this.props.raitingid + "rating"}
                               value="1" />
                        <label htmlFor={this.props.raitingid + "-1"} title="Sucks big time">1 star</label>
                    </div>
                </div>
            </div>
        )
    }
})

module.exports = SurveyStarRating;