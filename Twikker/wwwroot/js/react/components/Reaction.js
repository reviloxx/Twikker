import React from 'react';

export default class Reaction extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            textId: this.props.textId,
            reactions: this.props.reactions,
            activeUserId: this.props.activeUserId,
            activeUserAlreadyLiked: this.checkIfActiveUserAlreadyLiked(),
            iconSrc: this.checkIfActiveUserAlreadyLiked() ? "Images/liked.png" : "Images/like.png"
        }

        console.log(this.state);
    }

    checkIfActiveUserAlreadyLiked() {
        var count = this.props.reactions.filter((e) => e.creatorId == this.props.activeUserId).length;

        return count > 0;
    }
    
    handleLikeClick(e) {
        e.preventDefault();

        var data = new FormData();
        var xhr = new XMLHttpRequest();

        if (this.state.activeUserAlreadyLiked) {
            var newReactions = this.state.reactions;
            newReactions.splice(-1, 1);
            this.setState({ reactions: newReactions });
            this.setState({ iconSrc: "Images/like.png" });
            this.setState({ activeUserAlreadyLiked: false });

            data.append('textId', this.props.textId);
            xhr.open('post', "reactions/delete", true);
        } else {     
            var newReactions = this.state.reactions.slice();
            newReactions.push({ reaction: "like" });
            this.setState({ reactions: newReactions });
            this.setState({ iconSrc: "Images/liked.png" });
            this.setState({ activeUserAlreadyLiked: true });

            data.append('textId', this.props.textId);
            xhr.open('post', "reactions/add", true);
        }               
        
        xhr.onload = function () {
            
        }.bind(this);
        xhr.send(data);
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