var React = require('react');
var ReactDOM = require('react-dom');

//appendDetails //grouptype "home"  //grouptitle "Home" //grouplist

class DocumentTab extends React.Component {
    handleSubmit() {
        //var cname = this.refs.cname.getDOMNode().value;
        //var ecount = this.refs.ecount.getDOMNode().value;
        //var hoffice = this.refs.hoffice.getDOMNode().value;
        //var newrow = {cname: cname, ecount: ecount, hoffice: hoffice };
        //this.props.onRowSubmit( newrow );
          
        //this.refs.cname.getDOMNode().value = '';
        //this.refs.ecount.getDOMNode().value = '';
        //this.refs.hoffice.getDOMNode().value = '';
        return false;
    }
    render() {
        var inputStyle = {padding:'12px'}
        return (
            <div className="row" style={{ marginTop: '10px' } }>
                <div className="col-sm-2">
                    <br />
                    <ReactBootstrap.Button onClick={ () => this.appendDetails(tabElement.Item) }>+ Add {tabElement.Label} </ReactBootstrap.Button>
                    <br /><br />
                    {this.state[tabElement.Item + "detailcount"].map(function (index) {
                        return (<CollapsePanelLinks key={tabElement.Item + "detaillinks" + index}
                                                    index={index}
                                                    grouptype={tabElement.Item}
                                                    grouplabel={tabElement.Label}
                                                    detaillist={this.state[tabElement.Item + "detaillist"]}
                                                    handlePanelChange={this.handlePanelChange}
                                                    handleShowDetails={this.handleShowDetails}
                                                    handleRemovePanel={this.handleRemovePanel} />);
                    }, this)}
                </div>
                <div className="col-sm-1"></div>
                <div className="col-sm-8">
                    <br /><br />
                    {this.state[tabElement.Item + "detailcount"].map(function (index) {
                        var listhtml = [];
                        this.state[tabElement.Item + "detaillist"].map(function (obj) {
                            listhtml.push(
                            <ReactBootstrap.Collapse in={this.state[tabElement.Item + obj.Item + "detailpanel" + index]} key={tabElement.Item + obj.Item + "detailpanel" + index}>
                                <div>
                                <h3>{obj.Label} Information</h3>
                                <InputDetailList data={this.state["serverloaded" + count++]}
                                                 detaillist={this.state[obj.Item + "detail"]}
                                                 detailtype={tabElement.Item + obj.Item + "detail"}
                                                 count={index}
                                                 onChange={this.handleFieldChange} />
                                </div>
                            </ReactBootstrap.Collapse>);
                        }, this);
                        return (<div key={tabElement.Item + "detaillist" + index }>{listhtml}</div>)
                    }, this)}
                </div>
        </div>
        );
    }
}



module.exports = DocumentTab;
