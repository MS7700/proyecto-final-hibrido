import {
  useNotify,
  useRedirect,
  fetchStart,
  fetchEnd,
  Button,
  TopToolbar,
  DeleteButton,
  NumberInput,
  ReferenceInput,
  SelectInput,
} from "react-admin";

import { useState } from "react";
import { useDispatch } from "react-redux";

import SendIcon from "@material-ui/icons/Send";
import { makeStyles } from "@material-ui/core/styles";

import { useGetOne } from "react-admin";
import { useForm } from "react-final-form";

//Asientos contables
export const EnviarDeleteActions = ({ basePath, data, resource }) => (
  <TopToolbar>
    <EnviarButton record={data} />
    <DeleteButton basePath={basePath} record={data} resource={resource} />
  </TopToolbar>
);

const useStyles = makeStyles({
  button: {
    fontWeight: "bold",
    color: "green",
    "& svg": { color: "green" },
  },
});

export const EnviarButton = ({ record }) => {
  const classes = useStyles();
  const dispatch = useDispatch();
  const redirect = useRedirect();
  const notify = useNotify();
  const [loading, setLoading] = useState(false);
  const handleClick = () => {
    setLoading(true);
    dispatch(fetchStart()); // start the global loading indicator
    const Record = { ...record };
    fetch(
      `https://localhost:44340/AsientoContable(${record.id})/Contabilidad.EnviarAsiento`,
      { method: "POST", body: Record }
    )
      .then(() => {
        notify("Asiento enviado a contabilidad");
        redirect("/AsientoContable");
      })
      .catch((e) => {
        notify(
          "Error: El asiento no pudo ser enviado correctamente",
          "warning"
        );
      })
      .finally(() => {
        setLoading(false);
        dispatch(fetchEnd()); // stop the global loading indicator
      });
  };
  return (
    <Button
      children={<SendIcon />}
      className={classes.button}
      label="Enviar"
      onClick={handleClick}
      disabled={loading}
    />
  );
};

//Transacciones
const GetData = (id, recurso) => {
    // eslint-disable-next-line
  const { data, loading, error } = useGetOne(recurso, id);
  return data ? data : undefined;
};

export const TipoInput = ({ tipo }) => {
  let myTipo;
  if (tipo === "Deducción") {
    myTipo = "Deduccion";
  } else {
    myTipo = tipo;
  }
  return (
    <ReferenceInput
      label={"Tipo de " + tipo}
      source={"Tipo" + myTipo + "ID"}
      reference={"Tipo" + myTipo}
    >
      <SelectInput label={"Tipo" + myTipo} optionText="Nombre" />
    </ReferenceInput>
  );
};

export const MontoInput = ({ EmpleadoID, tipoID, tipoResource }) => {
  let mytipoResource;
  if (tipoResource === "TipoDeducción") {
    mytipoResource = "TipoDeduccion";
  } else {
    mytipoResource = tipoResource;
  }
  const empleado = GetData(EmpleadoID, "Empleado");
  const tipo = GetData(tipoID, mytipoResource);

  const form = useForm();
  let monto = 0;
  if (empleado && tipo) {
    if (tipo.Porcentual) {
      monto = empleado.Salario * (tipo.Cantidad / 100);
    } else {
      monto = tipo.Cantidad;
    }
    if (tipo.Automatico) {
      form.change("Monto", monto);
      return <NumberInput label="Monto" source="Monto" disabled={true} />;
    }
    return <NumberInput label="Monto" source="Monto" />;
  }
  return <div></div>;
};
