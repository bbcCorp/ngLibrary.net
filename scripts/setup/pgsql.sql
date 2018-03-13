SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

/*  ------------- Create Database and Schema ------------------------ */

CREATE DATABASE "ngLibrary"
  WITH ENCODING='UTF8'
       OWNER=postgres
       CONNECTION LIMIT=-1;


CREATE SCHEMA nglib;
ALTER SCHEMA nglib OWNER TO postgres;

SET search_path = nglib, pg_catalog;

/*  ------------- Creates Application Tables ------------------------ */

CREATE TABLE nglib.books (
    id integer NOT NULL,
    title character varying(1024) NOT NULL,
    description text NOT NULL,
    isbn character varying(255) NOT NULL,
    isbn13 character varying(255) NOT NULL,
    authors character varying(1024) NOT NULL,
    publisher character varying(1024) NOT NULL,
    publication_year integer,
    created_date timestamp with time zone DEFAULT clock_timestamp() NOT NULL,
    created_by character varying(256),
    update_tstamp timestamp with time zone DEFAULT clock_timestamp() NOT NULL
);

/*  ------------- Creates Users and Roles ------------------------ */

CREATE ROLE nglibapp with LOGIN PASSWORD 'INSERT_PASSWORD_HERE';
CREATE ROLE nglibadmin WITH LOGIN PASSWORD 'INSERT_PASSWORD_HERE' CREATEDB CREATEROLE;

GRANT USAGE ON SCHEMA nglib TO nglibadmin ;
GRANT USAGE ON SCHEMA nglib TO nglibapp ;

GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA nglib TO nglibadmin;

GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA nglib TO nglibapp;
