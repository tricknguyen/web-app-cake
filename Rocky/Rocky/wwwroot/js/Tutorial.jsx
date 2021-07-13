import CountrySelector from "./Covid/CountrySelector";
import HighLight from "./Covid/HighLight";
import Summary from "./Covid/Summary";

class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">
                Hello, world! I am a CommentBox.
            </div>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));