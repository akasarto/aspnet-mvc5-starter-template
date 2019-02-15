# About
Project online documentation source.

## Running locally

Make sure you have the following dependencies installed:

- python
- pip
- sphinx
  ```
  pip install sphinx
  ```
- sphinx_rtd_theme
  ```
  pip install sphinx_rtd_theme
  ```
- sphinx-autobuild
  ```
  pip install sphinx-autobuild
  ```

And then:

```
cd project/extracted/or/cloned/folder
```

```
sphinx-autobuild docs/source/ docs/build/
```

If everything goes as expected, the docs will be available at  `http://localhost:8000`.