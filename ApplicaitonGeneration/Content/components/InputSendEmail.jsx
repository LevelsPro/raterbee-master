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
            value: this.props.value
        };
        this.handleChange = this.handleChange.bind(this);
    }
    componentDidMount() {
        this.props.onChange(this.props.id, this.props.value);
    }
    handleChange(event) {
        var text = event.target.value;
        this.props.onChange(this.props.id, text);
        this.setState({ value: event.target.value });
    }
    render() {
        return (
            <div key={this.props.id} id={this.props.id }>
                <div className="row">
                    <div className="col-sm-4">
                        <input style={inputStyle}
                               className="form-control"
                               type="text"
                               placeholder={this.props.name}
                               onChange={this.handleChange}
                               value={this.state.value} />
                    </div>
                    <Button onClick={this.buttonOnClick}>Save & Continue</Button>
                </div>
            </div>);
    }
}

module.exports = InputDetailText;