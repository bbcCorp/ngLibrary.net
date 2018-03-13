SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;


SET search_path = nglib, pg_catalog;

INSERT INTO nglib.books (id, title, description, isbn, isbn13, authors, publisher, publication_year, created_by)
values (1, 'The Complete Sherlock Holmes', 'Perfect for mystery lovers, this anthology collects together the only four full-length novels starring the super sleuth Sherlock Holmes. Published in 1887, A Study in Scarlet was the first novel to feature Holmes and presents a grisly murder to the detective and his new-found companion, Dr. Watson. In The Sign of Four, Holmes must solve a perplexing case involving a damsel in distress, intrigue in colonial India, stolen treasure, a baffling murder, and four despicable ex-convicts. The Hound of the Baskervilles--perhaps the most famous of all Sherlock Holmes titles--features bizarre behavior and mysterious deaths on the Devon moors. In The Valley of Fear, Holmes unravels the mystery of a dead man''s mistaken identity and faces up to his old foe, Professor Moriarty.', '1853758884', '9781853758881', 'Arthur Conan Doyle', 'Random House', 1927, 'bbc' );

