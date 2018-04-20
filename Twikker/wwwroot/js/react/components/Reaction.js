import React from 'react';

export default class Reaction extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            textId: this.props.textId,
            reactions: this.props.reactions,
            activeUserId: this.props.activeUserId
        }

        console.log(this.props);
        console.log(this.state);
    }

    handleLikeClick(e) {
        e.preventDefault();

        var data = new FormData();
        var xhr = new XMLHttpRequest();

        data.append('textId', this.props.textId);
        xhr.open('post', "reactions/add", true);       
        
        xhr.onload = function () {
            var oldReactions = this.state.reactions.slice();
            oldReactions.push({ reaction: "like" });
            this.setState({ reactions: oldReactions });
        }.bind(this);
        xhr.send(data);
    }

    render() {
        if (this.state.activeUserId > -1) {
            return (
                <div className="reaction">
                    <p className="reaction-counter">{this.state.reactions.length}</p>
                    <button className="reaction-button btn-info" onClick={this.handleLikeClick.bind(this)}> Like</button>
                </div>
            );
        } else {
            return (
                <div className="reaction">
                    <p className="reaction-counter">{this.state.reactions.length}</p>
                </div>
            );
        }        
    }
}