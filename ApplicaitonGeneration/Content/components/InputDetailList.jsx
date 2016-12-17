var React = require('react');
var InputDetailText = require('./InputDetailText');


var InputDetailList = React.createClass({
    getInitialState: function(){
        return {
            data: this.props.data,
            index: this.props.index
        }
    },
    handleChange: function (fieldId, text) {
        //this.props[fieldId] = text;
        this.props.onChange(fieldId, text);
    },
    componentDidMount: function () {
        var valueId = 0;
        if (this.state.data != undefined) { valueId = this.state.data.Id; }
        this.props.onChange(this.props.detailtype + "Id" + this.state.index, valueId);
        //console.log(this.props.detailtype + this.props.index + "Id = " + valueId);
    },
    componentWillReceiveProps: function(nextProps) {
        this.setState({
            data: nextProps.data,
            index: nextProps.index
        });
    },
    render() {
        return (<div>
            {this.props.detaillist.map(function (obj) {
                var defaultValue = "";
                if (obj.Name) {
                    if (this.state.data != undefined) {
                        defaultValue = this.state.data[obj.Name];
                        if (defaultValue == undefined || defaultValue == null) {
                            defaultValue = "";
                        }
                    }
                    if (obj.Type == "1") {
                        var field = this.props.detailtype + obj.Name + this.state.index;
                        return <InputDetailText key={field}
                                                name={obj.Name.match(/[A-Z][a-z]+/g).join(' ')}
                                                id={field}
                                                type="1"
                                                value={defaultValue}
                                                onChange={this.handleChange} />
                    }
                } else {
                    if (this.state.data != undefined) {
                        defaultValue = this.state.data[obj];
                        if (defaultValue == undefined || defaultValue == null) {
                            defaultValue = "";
                        }
                    }
                    var field = this.props.detailtype + obj + this.state.index;
                    //if (this.props[field] == null) { this.props[field] = ''; }
                    return <InputDetailText key={field}
                                            name={obj.match(/[A-Z][a-z]+/g).join(' ')}
                                            type="0"
                                            id={field}
                                            value={defaultValue}
                                            onChange={this.handleChange } />
                }
            }, this)
            }
        </div>
        );
    }
});


module.exports = InputDetailList;