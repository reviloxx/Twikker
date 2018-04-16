import React from 'react';

export default class PostForm extends React.Component {
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

        var xhr = new XMLHttpRequest();
        xhr.open('post', "posts/add", true);
        xhr.onload = function () {
            this.setState({ content: '' });
            this.props.onAddedPost();
        }.bind(this);
        xhr.send(data);
        
    }

    render() {
        if (this.state.content.length > 0) {
            return (
                <form className="post-form" onSubmit={this.handleSubmit.bind(this)} >
                    <textarea className="form-control" rows="5"
                        type="text"
                        maxLength={300}
                        placeholder="Say something..."
                        value={this.state.content}
                        onChange={this.handleTextChange.bind(this)}
                    />
                    <input className="post-button btn-success" type="submit" value="Post" />
                    <p className="char-counter">
                        {300 - this.state.content.length}
                    </p>
                </form>
            );
        } else {
            return (
                <form className="post-form" onSubmit={this.handleSubmit.bind(this)} >
                    <textarea className="form-control" rows="5"
                        type="text"
                        maxLength={300}
                        placeholder="Say something..."
                        value={this.state.content}
                        onChange={this.handleTextChange.bind(this)}
                    />                
                    <input className="post-button btn-success" type="submit" value="Post" />
                </form>
            );
        }
    }
}