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
        var bs = '3px 3px 5px ' + this.props.backgroundshadow;
        return (
            <div className="row" style={{
                boxShadow: bs,
                borderRadius: '10px',
                paddingTop: '10px',
                paddingBottom: '10px',
                margin: '0px auto',
                maxWidth: '450px',
                border: '1px solid black',
                background: 'white'
            }}>
                <div className="col-xs-7" style={column}>
                    <div style={titleText} className="">{this.props.question}</div>
                </div>
                <div className="col-xs-5">
                    <div className="rating">
                        <input onChange={this.handleChange}
                               type="radio" id={this.props.raitingid + "-4"}
                               name={this.props.raitingid + "rating"}
                               value="4" />
                        <label htmlFor={this.props.raitingid + "-4"}>4 stars</label>

                        <input onChange={this.handleChange}
                               type="radio" id={this.props.raitingid + "-3"}
                               name={this.props.raitingid + "rating"}
                               value="3" />
                        <label htmlFor={this.props.raitingid + "-3"}>3 stars</label>

                        <input onChange={this.handleChange}
                               type="radio" id={this.props.raitingid + "-2"}
                               name={this.props.raitingid + "rating"}
                               value="2" />
                        <label htmlFor={this.props.raitingid + "-2"}>2 stars</label>

                        <input onChange={this.handleChange}
                               type="radio"
                               id={this.props.raitingid + "-1"}
                               name={this.props.raitingid + "rating"}
                               value="1" />
                        <label htmlFor={this.props.raitingid + "-1"}>1 star</label>
                    </div>
                </div>
            </div>
        )
    }
})

module.exports = SurveyStarRating;