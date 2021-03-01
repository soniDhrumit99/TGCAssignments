import "./App.css";
import Header from "./Components/Header/Header";
import Body from "./Components/Body/Body";
import Footer from "./Components/Footer/Footer";

function App() {
  return (
    <div className="App">
      <Header title="Prop heading" />
      <Body />
      <Footer />
    </div>
  );
}

export default App;
