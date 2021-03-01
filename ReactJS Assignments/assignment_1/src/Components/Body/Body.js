import React from "react";
import "./Body.css";

// function Body() {
//   const element = (
//     <div className="body">
//       <h1></h1>
//     </div>
//   );
//   return element;
// }

class Body extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      name: "Dhrumit Soni",
      college: "L. J. Institute of Engg. and Tech.",
    };
  }

  render() {
    return (
      <div className="body">
        <h3>
          Hello! My name is {this.state.name}
          <br />I am currently studying in {this.state.college}.
        </h3>
      </div>
    );
  }
}

export default Body;
