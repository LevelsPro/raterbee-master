var DatePicker = require("react-bootstrap-date-picker");
var React = require('react');

const inputStyle = {
    marginTop: '10px'
};
const labelStyle = {
    marginTop: '15px'
};

class InputDetailText extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: this.props.value,
            id: this.props.id,
            name: this.props.name,
            type: this.props.type
        };
        this.handleChange = this.handleChange.bind(this);
        this.handleDateChange = this.handleDateChange.bind(this);
    }
    componentDidMount() {
        this.props.onChange(this.state.id, this.state.value);
    }
    componentWillReceiveProps(nextProps) {
        if (nextProps.value != this.props.value ||
            nextProps.id != this.props.id) {
            this.setState({
                value: nextProps.value,
                id: nextProps.id,
                name: nextProps.name,
                type: nextProps.type
            });
        }
    }
    handleChange(event) {
        var text = event.target.value;
        this.props.onChange(this.state.id, text);
        this.setState({ value: event.target.value });
    }
    handleDateChange(value, formattedValue) {
        this.props.onChange(this.state.id, formattedValue);
        this.setState({
            value: value, // ISO String, ex: "2016-11-19T12:00:00.000Z"
            formattedValue: formattedValue // Formatted String, ex: "11/19/2016"
        });
    }
    render() {
        if (this.state.type == "1") {
            return (
            <div key={this.state.id} id={this.state.id }>
                <div className="row">
                    <div className="col-sm-4" style={labelStyle }>{this.state.name}</div>
                    <div className="col-sm-4" style={{ marginTop: '15px', width: '310px' } }>
                        <DatePicker id="" value={this.state.value} onChange={this.handleDateChange } />
                    </div>
                </div>
            </div>);
        } else if (this.state.type == "2") {
            return (
            <div key={this.state.id} id={this.state.id }>
                <div className="row">
                    <div className="col-sm-4" style={labelStyle }>{this.state.name}</div>
                    <div className="col-sm-8">
                        <input style={inputStyle}
                               className="form-control"
                               type="password"
                               placeholder={this.state.name}
                               onChange={this.handleChange}
                               value={this.state.value} />
                    </div>
                </div>
            </div>);
        } else {
            return (
            <div key={this.state.id} id={this.state.id }>
                <div className="row">
                    <div className="col-sm-4" style={labelStyle }>{this.state.name}</div>
                    <div className="col-sm-8">
                        <input style={inputStyle}
                               className="form-control"
                               type="text"
                               placeholder={this.state.name}
                               onChange={this.handleChange}
                               value={this.state.value} />
                    </div>
                </div>
            </div>);
        }
    }
}

module.exports = InputDetailText;