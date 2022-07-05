import React, { useEffect, useState } from "react";
import CountryDropdown from "./CountryDropdown";
import CityResponseDto from "../../Models/Cities/City/CityResponseDto";
import StateDropdown from "./StateDropdown";
import CityDropdown from "./CityDropdown";
import StateResponseDto from "../../Models/Cities/State/StateResponseDto";
import CountryResponseDto from "../../Models/Cities/Country/CountryResponseDto";
import citiesApi from "../../Api/citiesApi";
import CountrySearchRequestParams from "../../Models/Cities/Country/CountrySearchRequestParams";

const CityStateCountryDropdown = ({ cityId, selectedCity, handleChange }) => {
  console.log("city id passed: " + cityId);
  console.log("selected city: " + selectedCity?.name);

  const [country, setCountry] = useState();
  const [state, setState] = useState();
  const [isStateDropdownDisabled, setIsStateDropdownDisabled] = useState(true);
  const [isCityDropdownDisabled, setIsCityDropdownDisabled] = useState(true);

  const handleStateChange = (newValue) => {
    setState(newValue);

    if (newValue) {
      setIsCityDropdownDisabled(false);
    } else {
      setIsCityDropdownDisabled(true);
    }
    console.log("stateId: " + newValue?.stateId);
  };

  const handleCountryChange = (newValue) => {
    setCountry(newValue);

    setState(null);

    if (newValue) {
      setIsStateDropdownDisabled(false);
    } else {
      setIsStateDropdownDisabled(true);
    }
    console.log("countryId: " + newValue?.countryId);
  };

  useEffect(() => {
    initializeCityStateCountry();
  }, [cityId]);

  const initializeCityStateCountry = () => {
    if (cityId) {
      citiesApi
        .get("Cities/" + cityId)
        .then((res) => {
          const cityDetailResponse = res.data;
          console.log(cityDetailResponse);
          if (cityDetailResponse) {
            initializeCountry(cityDetailResponse.countryId);
          }
        })
        .catch((err) => {
          console.log(err);
        });
    }
  };

  const initializeCountry = (countryId) => {
    const countrySearchParams = new CountrySearchRequestParams(countryId, "");
    citiesApi
      .get("Countries/search", {
        params: countrySearchParams,
      })
      .then((res) => {
        const countryResponse = res.data.pagedList[0];
        setCountry(countryResponse);
        console.log("Initial country: " + countryResponse.name);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  return (
    <div>
      <CountryDropdown
        selectedCountry={country}
        handleChange={handleCountryChange}
      ></CountryDropdown>
      <StateDropdown
        countryId={country?.countryId}
        handleChange={handleStateChange}
        isDisabled={isStateDropdownDisabled}
        selectedState={state}
      ></StateDropdown>
      <CityDropdown
        stateId={state?.stateId}
        handleChange={handleChange}
        isDisabled={isCityDropdownDisabled}
        selectedCity={selectedCity}
      ></CityDropdown>
    </div>
  );
};

export default CityStateCountryDropdown;
