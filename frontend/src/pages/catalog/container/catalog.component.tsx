import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../../../store/store";
import { fetchProducts } from "../../../store/actions/catalog.actions";
import ListComponent from "../components/list.component";

const CatalogComponent: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  useEffect(() => {
    dispatch(fetchProducts());
  }, [dispatch]);
  return <ListComponent></ListComponent>;
};
export default CatalogComponent;
