import React, { Component, useEffect, useState } from "react";
import Select from "react-select";
import citiesApi from "../../Api/citiesApi";

const CountryDropdown = () => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState([]);

  const loadCountries = () => {
    citiesApi
      .get("Countries/search", {
        params: {
          searchText: inputValue,
        },
      })
      .then((res) => {
        setItems(res.data.pagedList);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadCountries();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleChange = (newValue) => {
    console.log(newValue);
  };
  const handleInputChange = (newValue) => {
    setInputValue(newValue);
    console.log(newValue);
  };

  return (
    <div>
      <Select
        getOptionLabel={(e) => e.name}
        getOptionValue={(e) => e.countryId}
        options={items}
        onChange={handleChange}
        onInputChange={handleInputChange}
      ></Select>
    </div>
  );
};

export default CountryDropdown;
