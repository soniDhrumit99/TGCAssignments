import "./Header.css";

function Header(props) {
  const element = (
    <div className="header">
      <h1>{props.title}</h1>
    </div>
  );
  return element;
}

export default Header;
