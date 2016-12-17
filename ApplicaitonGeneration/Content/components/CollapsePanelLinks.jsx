var React = require('react');
var ReactBootstrap = require('react-bootstrap');


const appendbuttonstyle = {
    marginTop: '10px',
    display: 'block',
    fontSize: "20px"
};
const subdetails = {
    marginTop: '10px'
};

class CollapsePanelLinks extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            collapsevalue: false
        };
        this.handlePanelChange = this.handlePanelChange.bind(this);
        this.handleShowDetails = this.handleShowDetails.bind(this);
        this.handleRemovePanel = this.handleRemovePanel.bind(this);

    }
    componentDidMount() {
        if (this.props.index == 0) {
            // Open first element in collapsable panel list
            this.handlePanelChange(this.props.grouptype, this.props.index);
        }
    }
    handleShowDetails(grouptype, detailtype, index) {
        this.props.handleShowDetails(grouptype, detailtype, index);
    }
    handlePanelChange(grouptype, index) {
        var open = !this.state.collapsevalue;
        this.props.handlePanelChange(grouptype, index, open);
        this.setState({ collapsevalue: open });
    }
    handleRemovePanel(index) {
        var r = confirm("Are you sure? This will remove this " + this.props.grouplabel + " from the list");
        if (r == true) {
            this.props.handleRemovePanel(this.props.grouptype, index);
        } else {
            // Not deleted
        }
    }
    render() {
        var listhtml = [];
        this.props.detaillist.map(function (obj) {
            listhtml.push(
                <div className="row" key={this.props.grouptype + obj.Item + "detail" + this.props.index} style={subdetails}>
                    <a onClick={ () => this.handleShowDetails(this.props.grouptype, obj.Item, this.props.index) }>{obj.Label}</a>
                </div>
            );
        }, this);
        return (
                <div style={{ paddingLeft: '11px' }}>
                    <div className="row">
                        <div className="col-xs-2" style={{ paddingTop: '15px' }}>
                            <a onClick={ () => this.handleRemovePanel(this.props.index) }
                               className="darkmagentaHover"
                               style={{ color: 'gray', paddingTop: '4px', paddingBottom: '4px', paddingLeft: '4px', paddingRight: '4px' }}>
                                <span className="glyphicon glyphicon-remove" />
                            </a>
                        </div>
                        <div className="col-xs-10">
                            <a style={appendbuttonstyle} onClick={ () => this.handlePanelChange(this.props.grouptype, this.props.index) }>{this.props.grouplabel}</a>
                            <ReactBootstrap.Collapse in={this.state.collapsevalue} key={this.props.grouptype + "detailpanel" + this.props.index}>
                                    <div className="row">
                                        <div className="col-sm-4"></div>
                                        <div className="col-sm-8">
                                            {listhtml}
                                        </div>
                                    </div>
                            </ReactBootstrap.Collapse>
                        </div>
                    </div>
                </div>
        )
    }
}

module.exports = CollapsePanelLinks;