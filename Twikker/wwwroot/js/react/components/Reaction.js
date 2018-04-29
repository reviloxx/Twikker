import React from 'react';
import * as ajaxhandler from '../ajax-handler';

export default class Reaction extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            textId: this.props.textId,
            reactions: this.props.reactions,
            activeUserId: this.props.activeUserId,
            activeUserAlreadyLiked: this.checkIfActiveUserAlreadyLiked(),
            iconSrc: this.checkIfActiveUserAlreadyLiked() ? "Images/liked.png" : "Images/like.png"
        };
    }

    checkIfActiveUserAlreadyLiked() {
        var count = this.props.reactions.filter((e) => e.creatorId === this.props.activeUserId).length;

        return count > 0;
    }
    
    handleLikeClick(e) {
        e.preventDefault();

        var data = new FormData();
        data.append('textId', this.props.textId);
        var newReactions;

        if (this.state.activeUserAlreadyLiked) {
            newReactions = this.state.reactions;
            newReactions.splice(-1, 1);
            this.setState({ reactions: newReactions });
            this.setState({ iconSrc: "Images/like.png" });
            this.setState({ activeUserAlreadyLiked: false });
            ajaxhandler.ajaxRequest(data, 'reactions/delete', null);
        } else {     
            newReactions = this.state.reactions.slice();
            newReactions.push({ reaction: "like" });
            this.setState({ reactions: newReactions });
            this.setState({ iconSrc: "Images/liked.png" });
            this.setState({ activeUserAlreadyLiked: true });
            ajaxhandler.ajaxRequest(data, 'reactions/add', null);
        }               
    }    

    render() {
        var reactionCounter = this.state.reactions.length > 0 ? <p className="reaction-counter">{this.state.reactions.length}</p> : <p className="reaction-counter" style={{ visibility: 'hidden' }}>-</p>;

        if (this.state.activeUserId > -1) {
            return (
                <div className="reaction">
                    <a className="reaction-button" href="#">
                        <img className="reaction-icon" src={this.state.iconSrc} alt="Like" onClick={this.handleLikeClick.bind(this)} />
                    </a>
                    {reactionCounter}
                </div>
            );
        } else if (this.state.reactions.length === 0) {
            return (
                <div className="reaction"/>
            );
        } else {
            return (
                <div className="reaction">
                    <a className="reaction-button" >
                        <img className="reaction-icon" src={this.state.iconSrc} alt="Like" />
                    </a>
                    {reactionCounter}
                </div>
            );
        }   
    }
}
Reaction.displayName = 'Reaction';