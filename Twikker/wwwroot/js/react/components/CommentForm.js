import React from 'react';

export default class CommentForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            content: ''
        };
    }

    handleTextChange(e) {
        this.setState({ content: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        var text = this.state.content.trim();
        if (!text) {
            return;
        }
        var data = new FormData();
        data.append('content', text);
        data.append('postId', this.props.postId);

        var xhr = new XMLHttpRequest();
        xhr.open('post', "comments/add", true);
        xhr.onload = function () {
            this.setState({ content: '' });
            this.props.onAddedComment();
        }.bind(this);
        xhr.send(data);
    }

    render() {
        return (
            <form className="comment-form" onSubmit={this.handleSubmit.bind(this)} >
                <textarea className="form-control" rows="2"
                    type="text"
                    placeholder="Say something..."
                    value={this.state.content}
                    onChange={this.handleTextChange.bind(this)}
                />
                <input className="comment-button btn-success" type="submit" value="Comment" />
            </form>
        );
    }
}