import { useSelector } from "react-redux";
import { getCarItems } from "../../../store/selectors/shoppingCart.selectors";
import { useNavigate } from "react-router-dom";

const NavBarComponent: React.FC = () => {
  const navigate = useNavigate();
  const carItems = useSelector(getCarItems);
  const handleButtonClick = () => navigate("/shoppingCart");
  const handleHomeClick = () => navigate("/");
  return (
    <nav className="navbar bg-body-tertiary fixed-top">
      <div className="container-fluid">
        <span onClick={handleHomeClick} className="navbar-brand mb-0 h1">
          Tienda Online
        </span>
        <button
          type="button"
          className="btn btn-primary position-relative me-5 mt-2"
          disabled={!carItems || carItems.length <= 0}
          onClick={handleButtonClick}
        >
          <i className="fa-solid fa-cart-shopping"></i>
          {carItems && carItems.length > 0 ? (
            <span className="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
              {carItems.length}
              <span className="visually-hidden">Car items</span>
            </span>
          ) : null}
        </button>
      </div>
    </nav>
  );
};

export default NavBarComponent;
