import React from "react";
import { Route, Routes } from "react-router-dom";
import LayoutComponent from "./pages/layout/container/layout.component";
const App: React.FC = () => {
  return (
    <Routes>
      <Route path="*" element={<LayoutComponent />} />
    </Routes>
  );
};

export default App;
